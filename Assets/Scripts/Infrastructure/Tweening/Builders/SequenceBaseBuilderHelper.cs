using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Infrastructure.Tweening.Builders
{
    public abstract class SequenceBaseBuilderHelper<TBuilder> : TweenBaseBuilderHelper<TBuilder>, ISequenceBaseBuilderHelper<TBuilder>
    {
        [NotNull, ItemNotNull] protected readonly ICollection<ITween> Tweens = new List<ITween>();

        public TBuilder AddTween([NotNull] ITween tween)
        {
            ArgumentNullException.ThrowIfNull(tween);

            Tweens.Add(tween);

            return This;
        }
    }
}