using System;
using UnityEngine;

namespace Game.Gameplay.View.Animation.Movement
{
    public interface IMovementHelper
    {
        void DoGravityMovement(Transform transform, int rowOffset, int columnOffset, Action onComplete);

        void DoLockMovement(Transform transform, int rowOffset, int columnOffset, Action onComplete);

        void DoInitialCameraMovement(Transform transform, int rowOffset, Action onComplete);

        void DoRegularCameraMovement(Transform transform, int rowOffset, Action onComplete);
    }
}