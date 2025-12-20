using JetBrains.Annotations;

namespace Infrastructure.Tweening.Builders
{
    public interface ITweenBuilder<TTarget, T> : ITweenBaseBuilderHelper<ITweenBuilder<TTarget, T>, ITween<TTarget, T>>
    {
        [NotNull]
        ITweenBuilder<TTarget, T> WithTarget(TTarget target);

        [NotNull]
        ITweenBuilder<TTarget, T> WithStart(T start);

        [NotNull]
        ITweenBuilder<TTarget, T> WithEnd(T end);

        [NotNull]
        ITweenBuilder<TTarget, T> WithDurationS(float durationS);

        [NotNull]
        ITweenBuilder<TTarget, T> WithEasingType(EasingType easingType);

        [NotNull]
        ITweenBuilder<TTarget, T> WithComplementaryEasingTypeBackwards(bool complementaryEasingTypeBackwards);
    }
}