using System;
using Game.Gameplay.View.Animation.Movement.Movements;
using Infrastructure.Tweening;
using Infrastructure.Unity.Utils;
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

            DoTweenMovement(transform, rowOffset, columnOffset, unitsPerSecond, EasingType.InQuad, onComplete);
        }

        public void DoLockMovement(Transform transform, int rowOffset, int columnOffset, Action onComplete)
        {
            const float unitsPerSecond = 60.0f;

            ArgumentNullException.ThrowIfNull(transform);

            DoTweenMovement(transform, rowOffset, columnOffset, unitsPerSecond, EasingType.InQuad, onComplete);
        }

        public void DoInitialCameraMovement([NotNull] Transform transform, int rowOffset, Action onComplete)
        {
            // TODO

            const int columnOffset = 0;
            const float unitsPerSecond = 15.0f;

            ArgumentNullException.ThrowIfNull(transform);

            DoTweenMovement(transform, rowOffset, columnOffset, unitsPerSecond, EasingType.InOutQuad, onComplete);
        }

        public void DoRegularCameraMovement([NotNull] Transform transform, int rowOffset, Action onComplete)
        {
            const int columnOffset = 0;
            const float unitsPerSecond = 15.0f;

            ArgumentNullException.ThrowIfNull(transform);

            DoTweenMovement(transform, rowOffset, columnOffset, unitsPerSecond, EasingType.InOutQuad, onComplete);
        }

        private void DoTweenMovement(
            [NotNull] Transform transform,
            int rowOffset,
            int columnOffset,
            float unitsPerSecond,
            EasingType easingType,
            Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(transform);

            Vector3 end = transform.position.AddX(columnOffset).AddY(rowOffset);

            ITweenMovement tweenMovement = _movementFactory.GetTweenMovement(transform, end, unitsPerSecond, onComplete);

            tweenMovement.TweenBuilder.WithEasingType(easingType);

            tweenMovement.Do();
        }
    }
}