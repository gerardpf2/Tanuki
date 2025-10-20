using System;
using Game.Gameplay.View.Animation.Movement.Movements;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Animation.Movement
{
    public interface IMovementFactory
    {
        [NotNull]
        ITweenMovement GetTweenMovement(Transform transform, int rowOffset, int columnOffset, Action onComplete);
    }
}