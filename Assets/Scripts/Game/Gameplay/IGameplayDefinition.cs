using System.Collections.Generic;
using Game.Gameplay.Goals;
using JetBrains.Annotations;

namespace Game.Gameplay
{
    public interface IGameplayDefinition
    {
        string Id { get; }

        string Board { get; }

        string Goals { get; }

        [NotNull, ItemNotNull]
        IEnumerable<IGoalDefinition> GoalDefinitions { get; }
    }
}