using JetBrains.Annotations;

namespace Game.Gameplay.View.Camera
{
    public interface ICameraView
    {
        [NotNull]
        UnityEngine.Camera UnityCamera { get; }

        void Initialize();

        void Uninitialize();

        void SetBoardViewLimits(float topPositionY, float bottomPositionY);
    }
}