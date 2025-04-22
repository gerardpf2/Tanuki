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
        ITweenBuilder<float> GetTweenBuilderFloat();

        [NotNull]
        ITweenBuilder<Vector3> GetTweenBuilderVector3();
    }
}