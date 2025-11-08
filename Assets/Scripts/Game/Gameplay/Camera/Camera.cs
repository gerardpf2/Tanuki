using Game.Common;

namespace Game.Gameplay.Camera
{
    public class Camera : ICamera
    {
        private int _topRow;
        private int _bottomRow;

        public int TopRow
        {
            get => _topRow;
            set
            {
                if (TopRow == value)
                {
                    return;
                }

                _topRow = value;
                _bottomRow = TopRow - VisibleRows + 1;
            }
        }

        public int BottomRow
        {
            get => _bottomRow;
            set
            {
                if (BottomRow == value)
                {
                    return;
                }

                _bottomRow = value;
                _topRow = BottomRow + VisibleRows - 1;
            }
        }

        public int VisibleRows => 15; // TODO: ScriptableObject

        public int ExtraRowsOnTop => 5; // TODO: ScriptableObject

        private InitializedLabel _initializedLabel;

        public Camera()
        {
            SetInitialBottomRow();
        }

        public void Initialize()
        {
            _initializedLabel.SetInitialized();

            SetInitialBottomRow();
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();
        }

        private void SetInitialBottomRow()
        {
            BottomRow = 0;
        }
    }
}