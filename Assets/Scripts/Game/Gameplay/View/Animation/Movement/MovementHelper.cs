using System;
using Game.Gameplay.View.Animation.Movement.Movements;
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
            ITweenMovement tweenMovement =
                _movementFactory.GetTweenMovement(
                    transform,
                    rowOffset,
                    columnOffset,
                    onComplete
                );

            // ITweenMovement::TweenBuilder can be updated if customization is needed

            tweenMovement.Do();
        }
    }
}