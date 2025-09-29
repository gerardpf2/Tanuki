using System.Collections.Generic;
using Game.Gameplay.Board;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Goals
{
    public class Goals : IGoals
    {
        [NotNull] private readonly IReadOnlyDictionary<PieceType, IGoal> _goals;

        public IEnumerable<IGoal> Targets => _goals.Values;

        public Goals([NotNull, ItemNotNull] IEnumerable<IGoal> goals)
        {
            ArgumentNullException.ThrowIfNull(goals);

            Dictionary<PieceType, IGoal> goalsCopy = new();

            foreach (IGoal goal in goals)
            {
                ArgumentNullException.ThrowIfNull(goal);

                if (!goalsCopy.TryAdd(goal.PieceType, goal))
                {
                    InvalidOperationException.Throw($"Cannot add goal with PieceType: {goal.PieceType}");
                }
            }

            _goals = goalsCopy;
        }
    }
}