using System;
using Game.Gameplay.Board;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Events.Reasons;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.Camera
{
    public class MoveCameraHelper : IMoveCameraHelper
    {
        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly ICamera _camera;

        public MoveCameraHelper([NotNull] IBoard board, [NotNull] ICamera camera)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(camera);

            _board = board;
            _camera = camera;
        }

        public MoveCameraEvent TargetHighestPlayerPieceLockRow(
            int pieceLockSourceCoordinateRow,
            MoveCameraReason moveCameraReason)
        {
            int prevCameraBottomRow = _camera.BottomRow;

            TargetHighestNonEmptyRow();
            TargetPieceLockRow(pieceLockSourceCoordinateRow);

            int newCameraBottomRow = _camera.BottomRow;
            int rowOffset = newCameraBottomRow - prevCameraBottomRow;

            MoveCameraEvent moveCameraEvent = new(rowOffset, moveCameraReason);

            return moveCameraEvent;
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