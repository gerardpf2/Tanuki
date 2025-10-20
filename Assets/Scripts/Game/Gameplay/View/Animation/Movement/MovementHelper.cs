using System;
using Game.Gameplay.View.Animation.Movement.Movements;
using Infrastructure.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.Animation.Movement
{
    public class MovementHelper : IMovementHelper
    {
        [NotNull] private readonly IMovementFactory _movementFactory;

        public MovementHelper([NotNull] IMovementFactory movementFactory)
        {
            ArgumentNullException.ThrowIfNull(movementFactory);

            _movementFactory = movementFactory;
        }

        public void DoGravityMovement(Transform transform, int rowOffset, int columnOffset, Action onComplete)
        {
            const float unitsPerSecond = 15.0f;

            ITweenMovement tweenMovement =
                _movementFactory.GetTweenMovement(
                    transform,
                    rowOffset,
                    columnOffset,
                    unitsPerSecond,
                    onComplete
                );

            tweenMovement.TweenBuilder.WithEasingType(EasingType.InQuad);

            tweenMovement.Do();
        }
    }
}