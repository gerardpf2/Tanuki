using JetBrains.Annotations;

namespace Infrastructure.Tweening.Builders
{
    public interface ISequenceBaseBuilderHelper<out TBuilder, out TTween> : ITweenBaseBuilderHelper<TBuilder, TTween>
    {
        [NotNull]
        TBuilder AddTween(ITweenBase tween);
    }
}