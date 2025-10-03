namespace Game.Gameplay.Camera
{
    public class CameraContainer : ICameraContainer
    {
        public ICamera Camera { get; private set; }

        public void Initialize(ICamera camera)
        {
            Uninitialize();

            Camera = camera;
        }

        public void Uninitialize()
        {
            Camera = null;
        }
    }
}