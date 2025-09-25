using System.Collections.Generic;
using JetBrains.Annotations;

namespace Game.Gameplay.Goals
{
    public interface IGoals
    {
        [NotNull, ItemNotNull]
        IEnumerable<IGoal> Targets { get; }
    }
}