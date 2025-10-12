using System;
using Game.Gameplay.Board;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.Camera
{
    public class Camera : ICamera
    {
        private const int ExtraRowsOnTop = 5; // TODO: ScriptableObject

        [NotNull] private readonly IBoardContainer _boardContainer;

        public int TopRow { get; private set; }

        public int BottomRow => TopRow - VisibleRows + 1;

        public int VisibleRows => 12; // TODO: ScriptableObject

        public Camera([NotNull] IBoardContainer boardContainer)
        {
            ArgumentNullException.ThrowIfNull(boardContainer);

            _boardContainer = boardContainer;

            TopRow = VisibleRows - 1;
        }

        public bool UpdateRow()
        {
            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            int newTopRow = Math.Max(board.HighestNonEmptyRow + ExtraRowsOnTop, VisibleRows - 1);

            if (TopRow == newTopRow)
            {
                return false;
            }

            TopRow = newTopRow;

            return true;
        }
    }
}