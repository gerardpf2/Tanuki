using JetBrains.Annotations;

namespace Infrastructure.Tweening.Builders
{
    public interface ISequenceBaseBuilderHelper<out TBuilder> : ITweenBaseBuilderHelper<TBuilder>
    {
        [NotNull]
        TBuilder AddTween(ITween tween);
    }
}