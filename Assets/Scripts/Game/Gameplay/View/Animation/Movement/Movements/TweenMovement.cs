using System;
using Infrastructure.Tweening;
using Infrastructure.Tweening.BuilderHelpers;
using Infrastructure.Tweening.Builders;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.Animation.Movement.Movements
{
    public class TweenMovement : ITweenMovement
    {
        [NotNull] private readonly ITweenRunner _tweenRunner;

        public ITweenBuilder<Vector3> TweenBuilder { get; }

        public TweenMovement(
            [NotNull] ITransformTweenBuilderHelper transformTweenBuilderHelper,
            [NotNull] ITweenRunner tweenRunner,
            [NotNull] Transform transform,
            Vector3 end,
            float unitsPerSecond,
            Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(transformTweenBuilderHelper);
            ArgumentNullException.ThrowIfNull(tweenRunner);
            ArgumentNullException.ThrowIfNull(transform);

            _tweenRunner = tweenRunner;

            float durationS = GetDurationS(transform.position, end, unitsPerSecond);

            TweenBuilder = transformTweenBuilderHelper.Move(transform, end, durationS).WithOnComplete(onComplete);
        }

        public void Do()
        {
            ITween tween = TweenBuilder.Build();

            _tweenRunner.Run(tween);
        }

        private static float GetDurationS(Vector3 start, Vector3 end, float unitsPerSecond)
        {
            float units = (end - start).magnitude;

            return units / unitsPerSecond;
        }
    }
}