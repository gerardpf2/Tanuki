using System;
using System.Collections.Generic;
using Game.Common;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.Camera;
using Game.Gameplay.Events;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.Phases.Phases
{
    public class LineClearPhase : Phase
    {
        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly ICamera _camera;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IDamagePieceHelper _damagePieceHelper;

        public LineClearPhase(
            [NotNull] IBoard board,
            [NotNull] ICamera camera,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IDamagePieceHelper damagePieceHelper)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(camera);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(damagePieceHelper);

            _board = board;
            _camera = camera;
            _eventEnqueuer = eventEnqueuer;
            _damagePieceHelper = damagePieceHelper;
        }

        protected override ResolveResult ResolveImpl(ResolveContext _)
        {
            IDictionary<int, DamagePieceEvent> damagePieceEventsByPieceId = new Dictionary<int, DamagePieceEvent>();

            int bottomRow = _camera.BottomRow;
            int topRow = Math.Min(_board.HighestNonEmptyRow, _camera.TopRow);

            for (int row = bottomRow; row <= topRow; ++row)
            {
                IEnumerable<DamagePieceEvent> damagePieceEvents = TryDamageRow(row);

                foreach (DamagePieceEvent damagePieceEvent in damagePieceEvents)
                {
                    damagePieceEventsByPieceId[damagePieceEvent.PieceId] = damagePieceEvent;
                }
            }

            if (damagePieceEventsByPieceId.Count <= 0)
            {
                return ResolveResult.NotUpdated;
            }

            DamagePiecesByLineClearEvent damagePiecesByLineClearEvent = new(damagePieceEventsByPieceId.Values);

            _eventEnqueuer.Enqueue(damagePiecesByLineClearEvent);

            return ResolveResult.Updated;
        }

        [NotNull, ItemNotNull]
        private IEnumerable<DamagePieceEvent> TryDamageRow(int row)
        {
            IReadOnlyCollection<KeyValuePair<int, int>> pieceIdsInRow =
                new List<KeyValuePair<int, int>>(
                    _board.GetPieceIdsInRow(row)
                );

            if (pieceIdsInRow.Count < _board.Columns)
            {
                yield break;
            }

            foreach ((int pieceId, int column) in pieceIdsInRow)
            {
                DamagePieceEvent damagePieceEvent =
                    _damagePieceHelper.Damage(
                        pieceId,
                        new Coordinate(row, column),
                        DamagePieceReason.LineClear,
                        Direction.Right
                    );

                yield return damagePieceEvent;
            }
        }
    }
}