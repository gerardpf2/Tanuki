using System;

namespace Game.Gameplay.Camera
{
    public class Camera : ICamera
    {
        private const int ExtraRowsOnTop = 5; // TODO: ScriptableObject
        private const int VisibleRows = 10; // TODO: ScriptableObject

        public int TopRow { get; private set; } = VisibleRows - 1;

        public int BottomRow => TopRow - VisibleRows + 1;

        public bool Update(int highestNonEmptyRow)
        {
            int newTopRow = Math.Max(highestNonEmptyRow + ExtraRowsOnTop, VisibleRows) - 1;

            if (TopRow == newTopRow)
            {
                return false;
            }

            TopRow = newTopRow;

            return true;
        }

        public ICamera Clone()
        {
            return new Camera { TopRow = TopRow };
        }
    }
}