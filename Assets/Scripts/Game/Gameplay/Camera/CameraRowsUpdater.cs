using System;
using Game.Gameplay.Board;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.Camera
{
    public class CameraRowsUpdater : ICameraRowsUpdater
    {
        private const int ExtraRowsOnTop = 5;
        private const int ExtraRowsOnBottom = 0;

        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly ICamera _camera;

        public CameraRowsUpdater([NotNull] IBoardContainer boardContainer, [NotNull] ICamera camera)
        {
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(camera);

            _boardContainer = boardContainer;
            _camera = camera;
        }

        public void TargetHighestNonEmptyRow()
        {
            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            int prevTopRow = _camera.TopRow;
            int newTopRow = Math.Max(board.HighestNonEmptyRow + ExtraRowsOnTop, _camera.VisibleRows - 1);

            if (prevTopRow == newTopRow)
            {
                return;
            }

            _camera.TopRow = newTopRow;
        }

        public void TargetLockRow(int lockRow)
        {
            int prevBottomRow = _camera.BottomRow;
            int newBottomRow = Math.Max(lockRow - ExtraRowsOnBottom, 0);

            if (prevBottomRow <= newBottomRow)
            {
                return;
            }

            _camera.BottomRow = newBottomRow;
        }
    }
}