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
            int rowOffset,
            int columnOffset,
            Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(transformTweenBuilderHelper);
            ArgumentNullException.ThrowIfNull(tweenRunner);
            ArgumentNullException.ThrowIfNull(transform);

            _tweenRunner = tweenRunner;

            Vector3 end = GetEnd(transform.position, rowOffset, columnOffset);
            float durationS = GetDurationS(rowOffset, columnOffset);

            TweenBuilder = transformTweenBuilderHelper.Move(transform, end, durationS).WithOnComplete(onComplete);
        }

        public void Do()
        {
            ITween tween = TweenBuilder.Build();

            _tweenRunner.Run(tween);
        }

        private static Vector3 GetEnd(Vector3 start, int rowOffset, int columnOffset)
        {
            float y = start.y + rowOffset;
            float x = start.x + columnOffset;

            return new Vector3(x, y, start.z);
        }

        private static float GetDurationS(int rowOffset, int columnOffset)
        {
            float hypotenuse = Mathf.Sqrt(rowOffset * rowOffset + columnOffset * columnOffset);

            // TODO

            return hypotenuse;
        }
    }
}