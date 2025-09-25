namespace Game.Gameplay.View.Camera
{
    public interface ICameraController
    {
        int VisibleTopRow { get; }

        void Initialize();

        void Uninitialize();

        void SetBoardViewLimits(float topPositionY, float bottomPositionY);
    }
}