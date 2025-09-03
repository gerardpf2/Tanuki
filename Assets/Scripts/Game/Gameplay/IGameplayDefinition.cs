using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Goals;
using JetBrains.Annotations;

namespace Game.Gameplay
{
    public interface IGameplayDefinition
    {
        string Id { get; }

        [NotNull]
        IBoardDefinition BoardDefinition { get; }

        [NotNull, ItemNotNull]
        IEnumerable<IGoalDefinition> GoalDefinitions { get; }
    }
}