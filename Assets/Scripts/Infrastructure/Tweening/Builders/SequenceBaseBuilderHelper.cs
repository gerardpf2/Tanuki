using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Infrastructure.Tweening.Builders
{
    public abstract class SequenceBaseBuilderHelper<TBuilder, TTween> : TweenBaseBuilderHelper<TBuilder, TTween>, ISequenceBaseBuilderHelper<TBuilder, TTween>
    {
        [NotNull, ItemNotNull] private readonly ICollection<ITweenBase> _tweens = new List<ITweenBase>(); // ItemNotNull as long as all Add check for null

        [NotNull, ItemNotNull] public IEnumerable<ITweenBase> Tweens => _tweens;

        public TBuilder AddTween([NotNull] ITweenBase tween)
        {
            ArgumentNullException.ThrowIfNull(tween);

            _tweens.Add(tween);

            return This;
        }
    }
}