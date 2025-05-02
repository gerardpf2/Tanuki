using Game.Gameplay.Board;

namespace Game.Gameplay.View.Camera
{
    public interface ICameraController
    {
        void Initialize(IReadonlyBoard board, float boardViewTopY, float boardViewBottomY);
    }
}