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

            IReadOnlyCollection<int> fullRows = GetFullRows(bottomRow, topRow);

            foreach (int row in fullRows)
            {
                IEnumerable<DamagePieceEvent> damagePieceEvents = DamageRow(row);

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

        [NotNull]
        private IReadOnlyCollection<int> GetFullRows(int bottomRow, int topRow)
        {
            Queue<int> fullRows = new();

            for (int row = bottomRow; row <= topRow; ++row)
            {
                if (_board.IsRowFull(row))
                {
                    fullRows.Enqueue(row);
                }
            }

            return fullRows;
        }

        [NotNull, ItemNotNull]
        private IEnumerable<DamagePieceEvent> DamageRow(int row)
        {
            IEnumerable<KeyValuePair<int, Coordinate>> pieceIdsInRow = _board.GetPieceIdsInRow(row);

            foreach ((int pieceId, Coordinate coordinate) in pieceIdsInRow)
            {
                DamagePieceEvent damagePieceEvent =
                    _damagePieceHelper.Damage(
                        pieceId,
                        coordinate,
                        DamagePieceReason.LineClear,
                        Direction.Right
                    );

                yield return damagePieceEvent;
            }
        }
    }
}