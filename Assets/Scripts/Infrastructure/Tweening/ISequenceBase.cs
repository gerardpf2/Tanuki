using System.Collections.Generic;
using JetBrains.Annotations;

namespace Infrastructure.Tweening
{
    public interface ISequenceBase : ITweenBase
    {
        [NotNull, ItemNotNull]
        IEnumerable<ITweenBase> Tweens { get; }
    }
}