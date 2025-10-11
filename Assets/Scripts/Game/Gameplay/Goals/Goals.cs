using System.Collections.Generic;
using System.Linq;
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

        public IGoal Get(PieceType pieceType)
        {
            if (!TryGet(pieceType, out IGoal goal))
            {
                InvalidOperationException.Throw($"Cannot find goal with PieceType: {pieceType}");
            }

            return goal;
        }

        public bool TryGet(PieceType pieceType, out IGoal goal)
        {
            return _goals.TryGetValue(pieceType, out goal);
        }

        public IGoals Clone()
        {
            return new Goals(Targets.Select(goal => goal.Clone()));
        }
    }
}