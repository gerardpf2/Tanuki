using System;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Camera
{
    public interface ICameraView
    {
        event Action OnUnityCameraSizeUpdated;

        /*
         *
         * ExtraRowsOnBottom has no impact during phases resolution
         * It allows the view to render more rows than the expected ones
         * For example, it can be used to render half a row to see the board ground
         *
         */
        float ExtraRowsOnBottom { get; }

        [NotNull]
        UnityEngine.Camera UnityCamera { get; }

        void Initialize();

        void Uninitialize();

        void SetBoardViewLimits(float topPositionY, float bottomPositionY);
    }
}