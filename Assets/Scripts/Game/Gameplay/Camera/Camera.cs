using System;
using Game.Gameplay.Board;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.Camera
{
    public class Camera : ICamera
    {
        private const int ExtraRowsOnTop = 5; // TODO: Scriptable object
        private const int VisibleRows = 10; // TODO: Scriptable object

        [NotNull] private readonly IBoardContainer _boardContainer;

        public int TopRow { get; private set; }

        public int BottomRow => TopRow - VisibleRows + 1;

        public Camera([NotNull] IBoardContainer boardContainer)
        {
            ArgumentNullException.ThrowIfNull(boardContainer);

            _boardContainer = boardContainer;
        }

        public bool Update()
        {
            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            int highestNonEmptyRow = board.HighestNonEmptyRow;

            int newTopRow = Math.Max(highestNonEmptyRow + ExtraRowsOnTop, VisibleRows) - 1;

            if (TopRow == newTopRow)
            {
                return false;
            }

            TopRow = newTopRow;

            return true;
        }
    }
}