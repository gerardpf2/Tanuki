using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Infrastructure.Tweening.Builders
{
    public abstract class SequenceBaseBuilderHelper<TBuilder> : TweenBaseBuilderHelper<TBuilder>, ISequenceBaseBuilderHelper<TBuilder>
    {
        [NotNull, ItemNotNull] private readonly ICollection<ITween> _tweens = new List<ITween>();

        [NotNull, ItemNotNull] public IEnumerable<ITween> Tweens => _tweens;

        public TBuilder AddTween([NotNull] ITween tween)
        {
            ArgumentNullException.ThrowIfNull(tween);

            _tweens.Add(tween);

            return This;
        }
    }
}