using Infrastructure.Tweening.TweenBuilders;
using Infrastructure.Unity.Utils;
using JetBrains.Annotations;
using UnityEngine;
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

        public ITweenBuilder<Vector3> MoveX([NotNull] Transform transform, float end, float durationS)
        {
            ArgumentNullException.ThrowIfNull(transform);

            return Move(transform, transform.position.WithX(end), durationS);
        }

        public ITweenBuilder<Vector3> MoveY([NotNull] Transform transform, float end, float durationS)
        {
            ArgumentNullException.ThrowIfNull(transform);

            return Move(transform, transform.position.WithY(end), durationS);
        }

        public ITweenBuilder<Vector3> MoveZ([NotNull] Transform transform, float end, float durationS)
        {
            ArgumentNullException.ThrowIfNull(transform);

            return Move(transform, transform.position.WithZ(end), durationS);
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

        public ITweenBuilder<Vector3> LocalMoveX([NotNull] Transform transform, float end, float durationS)
        {
            ArgumentNullException.ThrowIfNull(transform);

            return LocalMove(transform, transform.localPosition.WithX(end), durationS);
        }

        public ITweenBuilder<Vector3> LocalMoveY([NotNull] Transform transform, float end, float durationS)
        {
            ArgumentNullException.ThrowIfNull(transform);

            return LocalMove(transform, transform.localPosition.WithY(end), durationS);
        }

        public ITweenBuilder<Vector3> LocalMoveZ([NotNull] Transform transform, float end, float durationS)
        {
            ArgumentNullException.ThrowIfNull(transform);

            return LocalMove(transform, transform.localPosition.WithZ(end), durationS);
        }

        #endregion
    }
}