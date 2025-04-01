using Infrastructure.System.Exceptions;
using Infrastructure.Tweening.TweenBuilders;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Tweening
{
    public class TweenBuilderFactory : ITweenBuilderFactory
    {
        [NotNull] private readonly IEasingFunctionGetter _easingFunctionGetter;

        public TweenBuilderFactory([NotNull] IEasingFunctionGetter easingFunctionGetter)
        {
            ArgumentNullException.ThrowIfNull(easingFunctionGetter);

            _easingFunctionGetter = easingFunctionGetter;
        }

        public ISequenceAsyncBuilder GetSequenceAsyncBuilder()
        {
            return new SequenceAsyncBuilder();
        }

        public ISequenceBuilder GetSequenceBuilder()
        {
            return new SequenceBuilder();
        }

        public ITweenBuilder<float> GetTweenBuilderFloat()
        {
            return new TweenBuilderFloat(_easingFunctionGetter);
        }

        public ITweenBuilder<Vector3> GetTweenBuilderVector3()
        {
            return new TweenBuilderVector3(_easingFunctionGetter);
        }
    }
}