using Infrastructure.Tweening.Builders;
using Infrastructure.Unity;
using Infrastructure.Unity.Utils;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;

namespace Infrastructure.Tweening.BuilderHelpers
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
                    .WithEasingMode(EasingMode.EaseOut)
                    .WithComplementaryEasingModeBackwards(true)
                    .Build();

            ITween moveYMiddleToEnd =
                Move(transform, end, 0.5f * durationS, Axis.Y)
                    .WithStart(middle)
                    .WithEasingMode(EasingMode.EaseIn)
                    .WithComplementaryEasingModeBackwards(true)
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

        public ITweenBuilder<Vector3> Rotate(
            [NotNull] Transform transform,
            Vector3 end,
            float durationS,
            RotationType rotationType = RotationType.Full,
            Axis axis = Axis.All)
        {
            ArgumentNullException.ThrowIfNull(transform);

            Vector3 start = transform.rotation.eulerAngles;

            end = GetRotateEnd(start, end, rotationType);

            return
                _tweenBuilderFactory.GetTweenBuilderVector3()
                    .WithStart(start)
                    .WithEnd(end)
                    .WithDurationS(durationS)
                    .WithSetter(value => transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.With(value, axis)));
        }

        public ITweenBuilder<Vector3> Scale(
            [NotNull] Transform transform,
            Vector3 end,
            float durationS,
            Axis axis = Axis.All)
        {
            ArgumentNullException.ThrowIfNull(transform);

            return
                _tweenBuilderFactory.GetTweenBuilderVector3()
                    .WithStart(transform.localScale)
                    .WithEnd(end)
                    .WithDurationS(durationS)
                    .WithSetter(value => transform.localScale = transform.localScale.With(value, axis));
        }

        private static Vector3 GetRotateEnd(Vector3 start, Vector3 end, RotationType rotationType)
        {
            switch (rotationType)
            {
                case RotationType.Full:
                    return end;
                case RotationType.Closest:
                {
                    Vector3 endA = end.Remainder(360.0f);
                    Vector3 endB = endA - endA.Sign() * 360.0f;

                    return start.ClosestByCoordinate(endA, endB);
                }
                default:
                    ArgumentOutOfRangeException.Throw(rotationType);
                    return Vector3.zero;
            }
        }
    }
}