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

        public void DoGravityMovement([NotNull] Transform transform, int rowOffset, int columnOffset, Action onComplete)
        {
            const float unitsPerSecond = 15.0f;

            ArgumentNullException.ThrowIfNull(transform);

            Vector3 end = GetEnd(transform, rowOffset, columnOffset); // TODO: Vector3Utils AddX + AddY

            ITweenMovement tweenMovement = _movementFactory.GetTweenMovement(transform, end, unitsPerSecond, onComplete);

            tweenMovement.TweenBuilder.WithEasingType(EasingType.InQuad); // TODO: Review

            tweenMovement.Do();
        }

        public void DoCameraMovement([NotNull] Transform transform, int rowOffset, Action onComplete)
        {
            const int columnOffset = 0;
            const float unitsPerSecond = 15.0f;

            ArgumentNullException.ThrowIfNull(transform);

            Vector3 end = GetEnd(transform, rowOffset, columnOffset); // TODO: Vector3Utils AddY

            ITweenMovement tweenMovement = _movementFactory.GetTweenMovement(transform, end, unitsPerSecond, onComplete);

            tweenMovement.TweenBuilder.WithEasingType(EasingType.InQuad); // TODO: Review

            tweenMovement.Do();
        }

        private static Vector3 GetEnd([NotNull] Transform transform, int rowOffset, int columnOffset)
        {
            ArgumentNullException.ThrowIfNull(transform);

            return transform.position + new Vector3(columnOffset, rowOffset);
        }
    }
}