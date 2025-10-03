namespace Game.Gameplay.View.Camera
{
    // TODO: Remove
    public interface ICameraController
    {
        int VisibleTopRow { get; }

        void Initialize();

        void Uninitialize();

        void SetBoardViewLimits(float topPositionY, float bottomPositionY);
    }
}