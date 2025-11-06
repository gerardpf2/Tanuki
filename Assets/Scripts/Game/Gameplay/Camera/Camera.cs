using Game.Common;

namespace Game.Gameplay.Camera
{
    public class Camera : ICamera
    {
        public int TopRow { get; set; }

        public int BottomRow => TopRow - VisibleRows + 1;

        public int VisibleRows => 15; // TODO: ScriptableObject

        public int ExtraRowsOnTop => 5; // TODO: ScriptableObject

        private InitializedLabel _initializedLabel;

        public Camera()
        {
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

        private void SetInitialTopRow()
        {
            TopRow = VisibleRows - 1;
        }
    }
}