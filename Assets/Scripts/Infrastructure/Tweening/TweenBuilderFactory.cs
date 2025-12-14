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

        public ITweenBuilder<float> GetTweenBuilderFloat([NotNull] Action<float> setter)
        {
            ArgumentNullException.ThrowIfNull(setter);

            return new TweenBuilderFloat(setter, _easingFunctionGetter);
        }

        public ITweenBuilder<Vector3> GetTweenBuilderVector3([NotNull] Action<Vector3> setter)
        {
            ArgumentNullException.ThrowIfNull(setter);

            return new TweenBuilderVector3(setter, _easingFunctionGetter);
        }
    }
}