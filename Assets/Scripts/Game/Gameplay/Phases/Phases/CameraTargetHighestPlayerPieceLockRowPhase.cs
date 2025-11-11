using System;
using Game.Gameplay.Board;
using Game.Gameplay.Camera;
using Game.Gameplay.Events;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.Phases.Phases
{
    public class CameraTargetHighestPlayerPieceLockRowPhase : Phase
    {
        // Targets the highest board row that allows the player piece ghost to be fully visible

        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly ICamera _camera;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;

        private Coordinate? _lastTargetedPieceLockSourceCoordinate;

        public CameraTargetHighestPlayerPieceLockRowPhase(
            [NotNull] IBoard board,
            [NotNull] ICamera camera,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(camera);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);

            _board = board;
            _camera = camera;
            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
        }

        protected override ResolveResult ResolveImpl([NotNull] ResolveContext resolveContext)
        {
            ArgumentNullException.ThrowIfNull(resolveContext);

            if (!resolveContext.PieceLockSourceCoordinate.HasValue)
            {
                return ResolveResult.NotUpdated;
            }

            Coordinate pieceLockSourceCoordinate = resolveContext.PieceLockSourceCoordinate.Value;

            if (_lastTargetedPieceLockSourceCoordinate.HasValue &&
                _lastTargetedPieceLockSourceCoordinate.Value.Equals(pieceLockSourceCoordinate))
            {
                return ResolveResult.NotUpdated;
            }

            _lastTargetedPieceLockSourceCoordinate = pieceLockSourceCoordinate;

            int prevCameraBottomRow = _camera.BottomRow;

            TargetHighestNonEmptyRow();
            TargetPieceLockRow(pieceLockSourceCoordinate.Row);

            int newCameraBottomRow = _camera.BottomRow;

            int rowOffset = newCameraBottomRow - prevCameraBottomRow;

            if (rowOffset == 0)
            {
                return ResolveResult.NotUpdated;
            }

            _eventEnqueuer.Enqueue(_eventFactory.GetMoveCameraEvent(rowOffset));

            return ResolveResult.Updated;
        }

        public override void OnEndIteration()
        {
            base.OnEndIteration();

            _lastTargetedPieceLockSourceCoordinate = null;
        }

        private void TargetHighestNonEmptyRow()
        {
            _camera.TopRow = _board.HighestNonEmptyRow + _camera.ExtraRowsOnTop;
        }

        private void TargetPieceLockRow(int pieceLockSourceCoordinateRow)
        {
            _camera.BottomRow = Math.Min(_camera.BottomRow, pieceLockSourceCoordinateRow);
        }
    }
}