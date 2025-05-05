using Game.Gameplay.Board;

namespace Game.Gameplay.View.Camera
{
    public interface ICameraController
    {
        void Initialize(IReadonlyBoard board);

        void SetBoardViewLimits(float topPositionY, float bottomPositionY);
    }
}