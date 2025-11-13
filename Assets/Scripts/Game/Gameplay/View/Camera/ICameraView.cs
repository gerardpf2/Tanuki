using System;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Camera
{
    public interface ICameraView
    {
        event Action OnUnityCameraSizeUpdated;

        [NotNull]
        UnityEngine.Camera UnityCamera { get; }

        void Initialize();

        void Uninitialize();

        void SetBoardViewLimits(float topPositionY, float bottomPositionY);
    }
}