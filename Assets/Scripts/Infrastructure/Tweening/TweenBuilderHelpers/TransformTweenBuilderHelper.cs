using Infrastructure.System.Exceptions;
using Infrastructure.Tweening.TweenBuilders;
using JetBrains.Annotations;
using UnityEngine;

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

        #region Movement

        public ITweenBuilder<Vector3> Move([NotNull] Transform transform, Vector3 end, float durationS)
        {
            ArgumentNullException.ThrowIfNull(transform);

            return
                _tweenBuilderFactory.GetTweenBuilderVector3()
                    .WithStart(transform.position)
                    .WithEnd(end)
                    .WithDurationS(durationS)
                    .WithSetter(value => transform.position = value);
        }

        public ITweenBuilder<float> MoveX([NotNull] Transform transform, float end, float durationS)
        {
            ArgumentNullException.ThrowIfNull(transform);

            return
                _tweenBuilderFactory.GetTweenBuilderFloat()
                    .WithStart(transform.position.x)
                    .WithEnd(end)
                    .WithDurationS(durationS)
                    .WithSetter(value =>
                        transform.position = new Vector3(
                            value,
                            transform.position.y,
                            transform.position.z
                        )
                    );
        }

        public ITweenBuilder<float> MoveY([NotNull] Transform transform, float end, float durationS)
        {
            ArgumentNullException.ThrowIfNull(transform);

            return
                _tweenBuilderFactory.GetTweenBuilderFloat()
                    .WithStart(transform.position.y)
                    .WithEnd(end)
                    .WithDurationS(durationS)
                    .WithSetter(value =>
                        transform.position = new Vector3(
                            transform.position.x,
                            value,
                            transform.position.z
                        )
                    );
        }

        public ITweenBuilder<float> MoveZ([NotNull] Transform transform, float end, float durationS)
        {
            ArgumentNullException.ThrowIfNull(transform);

            return
                _tweenBuilderFactory.GetTweenBuilderFloat()
                    .WithStart(transform.position.z)
                    .WithEnd(end)
                    .WithDurationS(durationS)
                    .WithSetter(value =>
                        transform.position = new Vector3(
                            transform.position.x,
                            transform.position.y,
                            value
                        )
                    );
        }

        public ITweenBuilder<Vector3> LocalMove([NotNull] Transform transform, Vector3 end, float durationS)
        {
            ArgumentNullException.ThrowIfNull(transform);

            return
                _tweenBuilderFactory.GetTweenBuilderVector3()
                    .WithStart(transform.localPosition)
                    .WithEnd(end)
                    .WithDurationS(durationS)
                    .WithSetter(value => transform.localPosition = value);
        }

        public ITweenBuilder<float> LocalMoveX([NotNull] Transform transform, float end, float durationS)
        {
            ArgumentNullException.ThrowIfNull(transform);

            return
                _tweenBuilderFactory.GetTweenBuilderFloat()
                    .WithStart(transform.localPosition.x)
                    .WithEnd(end)
                    .WithDurationS(durationS)
                    .WithSetter(value =>
                        transform.localPosition = new Vector3(
                            value,
                            transform.localPosition.y,
                            transform.localPosition.z
                        )
                    );
        }

        public ITweenBuilder<float> LocalMoveY([NotNull] Transform transform, float end, float durationS)
        {
            ArgumentNullException.ThrowIfNull(transform);

            return
                _tweenBuilderFactory.GetTweenBuilderFloat()
                    .WithStart(transform.localPosition.y)
                    .WithEnd(end)
                    .WithDurationS(durationS)
                    .WithSetter(value =>
                        transform.localPosition = new Vector3(
                            transform.localPosition.x,
                            value,
                            transform.localPosition.z
                        )
                    );
        }

        public ITweenBuilder<float> LocalMoveZ([NotNull] Transform transform, float end, float durationS)
        {
            ArgumentNullException.ThrowIfNull(transform);

            return
                _tweenBuilderFactory.GetTweenBuilderFloat()
                    .WithStart(transform.localPosition.z)
                    .WithEnd(end)
                    .WithDurationS(durationS)
                    .WithSetter(value =>
                        transform.localPosition = new Vector3(
                            transform.localPosition.x,
                            transform.localPosition.y,
                            value
                        )
                    );
        }

        #endregion
    }
}