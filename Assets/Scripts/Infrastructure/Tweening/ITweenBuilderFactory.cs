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
        ITweenBuilder<float> GetTweenBuilderFloat(Action<float> setter);

        [NotNull]
        ITweenBuilder<Vector3> GetTweenBuilderVector3(Action<Vector3> setter);
    }
}