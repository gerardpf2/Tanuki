using System;
using Game.Common;
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

        private InitializedLabel _initializedLabel;

        public int TopRow { get; private set; }

        public int BottomRow => TopRow - VisibleRows + 1;

        public int VisibleRows => 15; // TODO: ScriptableObject

        public Camera([NotNull] IBoardContainer boardContainer)
        {
            ArgumentNullException.ThrowIfNull(boardContainer);

            _boardContainer = boardContainer;

            SetInitialTopRow();
        }

        public void Initialize()
        {
            _initializedLabel.SetInitialized();

            SetInitialTopRow();
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();
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

        private void SetInitialTopRow()
        {
            TopRow = VisibleRows - 1;
        }
    }
}