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
            int bottomRow = _camera.BottomRow;
            int topRow = Math.Min(_board.HighestNonEmptyRow, _camera.TopRow);

            IReadOnlyCollection<Coordinate> coordinatesToDamage = GetCoordinatesToDamage(bottomRow, topRow);

            if (coordinatesToDamage.Count <= 0)
            {
                return ResolveResult.NotUpdated;
            }

            IEnumerable<DamagePieceEvent> damagePieceEvents = GetDamagePieceEvents(coordinatesToDamage);

            DamagePiecesByLineClearEvent damagePiecesByLineClearEvent = new(damagePieceEvents);

            _eventEnqueuer.Enqueue(damagePiecesByLineClearEvent);

            return ResolveResult.Updated;
        }

        [NotNull]
        private IReadOnlyCollection<Coordinate> GetCoordinatesToDamage(int bottomRow, int topRow)
        {
            List<Coordinate> coordinatesToDamage = new();

            for (int row = bottomRow; row <= topRow; ++row)
            {
                if (!_board.IsRowFull(row))
                {
                    continue;
                }

                IEnumerable<Coordinate> coordinatesInRow = _board.GetCoordinatesInRow(row);

                coordinatesToDamage.AddRange(coordinatesInRow);
            }

            return coordinatesToDamage;
        }

        [NotNull, ItemNotNull]
        private IEnumerable<DamagePieceEvent> GetDamagePieceEvents([NotNull] IEnumerable<Coordinate> coordinates)
        {
            ArgumentNullException.ThrowIfNull(coordinates);

            IEnumerable<DamagePieceEvent> damagePieceEvents =
                _damagePieceHelper.Damage(
                    coordinates,
                    DamagePieceReason.LineClear,
                    Direction.Right
                );

            return damagePieceEvents;
        }
    }
}