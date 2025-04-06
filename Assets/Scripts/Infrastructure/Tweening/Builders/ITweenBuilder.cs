using System;
using JetBrains.Annotations;

namespace Infrastructure.Tweening.Builders
{
    public interface ITweenBuilder<T> : ITweenBaseBuilderHelper<ITweenBuilder<T>>
    {
        [NotNull]
        ITweenBuilder<T> WithStart(T start);

        [NotNull]
        ITweenBuilder<T> WithEnd(T end);

        [NotNull]
        ITweenBuilder<T> WithDurationS(float durationS);

        [NotNull]
        ITweenBuilder<T> WithSetter(Action<T> setter);

        [NotNull]
        ITweenBuilder<T> WithEasingType(EasingType easingType);

        [NotNull]
        ITweenBuilder<T> WithComplementaryEasingTypeBackwards(bool complementaryEasingTypeBackwards);
    }
}