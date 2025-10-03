namespace Game.Gameplay.Camera
{
    public interface ICameraContainer
    {
        ICamera Camera { get; }

        void Initialize(ICamera camera);

        void Uninitialize();
    }
}