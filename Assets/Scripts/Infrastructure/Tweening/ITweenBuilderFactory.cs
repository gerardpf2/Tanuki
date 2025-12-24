using System;
using Infrastructure.Tweening.Builders;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Tweening
{
    public interface ITweenBuilderFactory
    {
        [NotNull]
        ISequenceAsyncBuilder GetSequenceAsyncBuilder();

        [NotNull]
        ISequenceBuilder GetSequenceBuilder();

        [NotNull]
        ITweenBuilder<TTarget, float> GetTweenBuilderFloat<TTarget>(Action<TTarget, float> setter);

        [NotNull]
        ITweenBuilder<TTarget, Vector3> GetTweenBuilderVector3<TTarget>(Action<TTarget, Vector3> setter);
    }
}