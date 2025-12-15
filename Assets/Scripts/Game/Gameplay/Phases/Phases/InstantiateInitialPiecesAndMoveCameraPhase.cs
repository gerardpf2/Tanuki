using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.Camera;
using Game.Gameplay.Events;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Phases.Phases
{
    public class InstantiateInitialPiecesAndMoveCameraPhase : Phase
    {
        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly IMoveCameraHelper _moveCameraHelper;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;

        protected override int? MaxResolveTimesPerIteration => 1;

        public InstantiateInitialPiecesAndMoveCameraPhase(
            [NotNull] IBoard board,
            [NotNull] IMoveCameraHelper moveCameraHelper,
            [NotNull] IEventEnqueuer eventEnqueuer)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(moveCameraHelper);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);

            _board = board;
            _moveCameraHelper = moveCameraHelper;
            _eventEnqueuer = eventEnqueuer;
        }

        protected override ResolveResult ResolveImpl([NotNull] ResolveContext resolveContext)
        {
            ArgumentNullException.ThrowIfNull(resolveContext);

            if (!resolveContext.PieceLockSourceCoordinate.HasValue)
            {
                return ResolveResult.NotUpdated;
            }

            IEnumerable<InstantiatePieceEvent> instantiatePieceEvents = GetInstantiatePieceEvents();
            MoveCameraEvent moveCameraEvent = GetMoveCameraEvent(resolveContext.PieceLockSourceCoordinate.Value.Row);

            InstantiateInitialPiecesAndMoveCameraEvent instantiateInitialPiecesAndMoveCameraEvent =
                new(
                    instantiatePieceEvents,
                    moveCameraEvent
                );

            _eventEnqueuer.Enqueue(instantiateInitialPiecesAndMoveCameraEvent);

            return ResolveResult.Updated;
        }

        [NotNull, ItemNotNull]
        private IEnumerable<InstantiatePieceEvent> GetInstantiatePieceEvents()
        {
            foreach (int pieceId in _board.GetDistinctPieceIdsSortedByRowThenByColumn())
            {
                IPiece piece = _board.GetPiece(pieceId);
                Coordinate sourceCoordinate = _board.GetSourceCoordinate(pieceId);

                InstantiatePieceEvent instantiatePieceEvent =
                    new(
                        piece,
                        sourceCoordinate,
                        InstantiatePieceReason.Initial
                    );

                yield return instantiatePieceEvent;
            }
        }

        [NotNull]
        private MoveCameraEvent GetMoveCameraEvent(int pieceLockSourceCoordinateRow)
        {
            MoveCameraEvent moveCameraEvent =
                _moveCameraHelper.TargetHighestPlayerPieceLockRow(
                    pieceLockSourceCoordinateRow,
                    MoveCameraReason.Initial
                );

            return moveCameraEvent;
        }
    }
}