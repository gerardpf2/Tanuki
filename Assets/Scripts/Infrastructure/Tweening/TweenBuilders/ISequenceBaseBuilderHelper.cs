using JetBrains.Annotations;

namespace Infrastructure.Tweening.TweenBuilders
{
    public interface ISequenceBaseBuilderHelper<out TBuilder> : ITweenBaseBuilderHelper<TBuilder>
    {
        [NotNull]
        TBuilder AddTween(ITween tween);
    }
}