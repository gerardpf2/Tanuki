using System;
using Game.Gameplay.View.Animation.Movement.Movements;
using Infrastructure.Tweening;
using Infrastructure.Tweening.BuilderHelpers;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.Animation.Movement
{
    public class MovementFactory : IMovementFactory
    {
        [NotNull] private readonly ITransformTweenBuilderHelper _transformTweenBuilderHelper;
        [NotNull] private readonly ITweenRunner _tweenRunner;

        public MovementFactory(
            [NotNull] ITransformTweenBuilderHelper transformTweenBuilderHelper,
            [NotNull] ITweenRunner tweenRunner)
        {
            ArgumentNullException.ThrowIfNull(transformTweenBuilderHelper);
            ArgumentNullException.ThrowIfNull(tweenRunner);

            _transformTweenBuilderHelper = transformTweenBuilderHelper;
            _tweenRunner = tweenRunner;
        }

        public ITweenMovement GetTweenMovement(
            [NotNull] Transform transform,
            int rowOffset,
            int columnOffset,
            float unitsPerSecond,
            Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(transform);

            return
                new TweenMovement(
                    _transformTweenBuilderHelper,
                    _tweenRunner,
                    transform,
                    rowOffset,
                    columnOffset,
                    unitsPerSecond,
                    onComplete
                );
        }
    }
}