using System;
using Infrastructure.Tweening.Builders;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

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

        public ITweenBuilder<TTarget, float> GetTweenBuilderFloat<TTarget>([NotNull] Action<TTarget, float> setter)
        {
            ArgumentNullException.ThrowIfNull(setter);

            return new TweenBuilderFloat<TTarget>(setter, _easingFunctionGetter);
        }

        public ITweenBuilder<TTarget, Vector3> GetTweenBuilderVector3<TTarget>([NotNull] Action<TTarget, Vector3> setter)
        {
            ArgumentNullException.ThrowIfNull(setter);

            return new TweenBuilderVector3<TTarget>(setter, _easingFunctionGetter);
        }
    }
}