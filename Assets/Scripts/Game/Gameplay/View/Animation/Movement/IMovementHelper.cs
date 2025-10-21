using System;
using UnityEngine;

namespace Game.Gameplay.View.Animation.Movement
{
    public interface IMovementHelper
    {
        void DoGravityMovement(Transform transform, int rowOffset, int columnOffset, Action onComplete);

        void DoCameraMovement(Transform transform, int rowOffset, Action onComplete);
    }
}