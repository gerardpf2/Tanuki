using System;
using Game.Gameplay.Common;
using Game.Gameplay.Common.Utils;
using Game.Gameplay.View.Animation.Movement.Movements;
using Infrastructure.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.Animation.Movement
{
    public class MovementHelper : IMovementHelper
    {
        [NotNull] private readonly IWorldPositionGetter _worldPositionGetter;
        [NotNull] private readonly IMovementFactory _movementFactory;

        public MovementHelper(
            [NotNull] IWorldPositionGetter worldPositionGetter,
            [NotNull] IMovementFactory movementFactory)
        {
            ArgumentNullException.ThrowIfNull(worldPositionGetter);
            ArgumentNullException.ThrowIfNull(movementFactory);

            _worldPositionGetter = worldPositionGetter;
            _movementFactory = movementFactory;
        }

        public void DoGravityMovement([NotNull] Transform transform, int rowOffset, int columnOffset, Action onComplete)
        {
            const float unitsPerSecond = 15.0f;

            ArgumentNullException.ThrowIfNull(transform);

            Vector3 end = GetEnd(transform, rowOffset, columnOffset);

            ITweenMovement tweenMovement = _movementFactory.GetTweenMovement(transform, end, unitsPerSecond, onComplete);

            tweenMovement.TweenBuilder.WithEasingType(EasingType.InQuad); // TODO: Review

            tweenMovement.Do();
        }

        public void DoCameraMovement([NotNull] Transform transform, int rowOffset, Action onComplete)
        {
            const int columnOffset = 0;
            const float unitsPerSecond = 15.0f;

            ArgumentNullException.ThrowIfNull(transform);

            Vector3 end = GetEnd(transform, rowOffset, columnOffset);

            ITweenMovement tweenMovement = _movementFactory.GetTweenMovement(transform, end, unitsPerSecond, onComplete);

            tweenMovement.TweenBuilder.WithEasingType(EasingType.InQuad); // TODO: Review

            tweenMovement.Do();
        }

        private Vector3 GetEnd([NotNull] Transform transform, int rowOffset, int columnOffset)
        {
            ArgumentNullException.ThrowIfNull(transform);

            Vector3 origin = _worldPositionGetter.Get(0, 0); // TODO: Cache
            Vector3 offset = _worldPositionGetter.Get(rowOffset, columnOffset) - origin;
            Vector3 start = transform.position;
            Vector3 end = start + offset;

            return end;
        }
    }
}