using Infrastructure.Tweening.TweenBuilders;
using Infrastructure.Unity;
using Infrastructure.Unity.Utils;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.Tweening.TweenBuilderHelpers
{
    public class TransformTweenBuilderHelper : ITransformTweenBuilderHelper
    {
        [NotNull] private readonly ITweenBuilderFactory _tweenBuilderFactory;

        public TransformTweenBuilderHelper([NotNull] ITweenBuilderFactory tweenBuilderFactory)
        {
            ArgumentNullException.ThrowIfNull(tweenBuilderFactory);

            _tweenBuilderFactory = tweenBuilderFactory;
        }

        public ITweenBuilder<Vector3> Move(
            [NotNull] Transform transform,
            Vector3 end,
            float durationS,
            Axis axis = Axis.All)
        {
            ArgumentNullException.ThrowIfNull(transform);

            return
                _tweenBuilderFactory.GetTweenBuilderVector3()
                    .WithStart(transform.position)
                    .WithEnd(end)
                    .WithDurationS(durationS)
                    .WithSetter(value => transform.position = transform.position.With(value, axis));
        }

        public ISequenceAsyncBuilder Jump([NotNull] Transform transform, Vector3 end, float height, float durationS)
        {
            ArgumentNullException.ThrowIfNull(transform);

            Vector3 start = transform.position;
            Vector3 middle = (0.5f * (start + end)).WithY(Mathf.Max(start.y, end.y) + height);

            ITween moveXZ =
                Move(transform, end, durationS, Axis.X | Axis.Z)
                    .WithEasingMode(EasingMode.Linear)
                    .Build();

            ITween moveYStartToMiddle =
                Move(transform, middle, 0.5f * durationS, Axis.Y)
                    .WithEasingMode(EasingMode.EaseOut, EasingMode.EaseIn)
                    .Build();

            ITween moveYMiddleToEnd =
                Move(transform, end, 0.5f * durationS, Axis.Y)
                    .WithStart(middle)
                    .WithEasingMode(EasingMode.EaseIn, EasingMode.EaseOut)
                    .Build();

            ITween moveY =
                _tweenBuilderFactory.GetSequenceBuilder()
                    .AddTween(moveYStartToMiddle)
                    .AddTween(moveYMiddleToEnd)
                    .Build();

            return
                _tweenBuilderFactory.GetSequenceAsyncBuilder()
                    .AddTween(moveXZ)
                    .AddTween(moveY);
        }
    }
}