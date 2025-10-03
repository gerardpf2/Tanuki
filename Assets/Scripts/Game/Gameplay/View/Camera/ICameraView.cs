namespace Game.Gameplay.View.Camera
{
    public interface ICameraView
    {
        void Initialize();

        void Uninitialize();

        void SetBoardViewLimits(float topPositionY, float bottomPositionY);

        void UpdatePosition(int topRow, int bottomRow);
    }
}