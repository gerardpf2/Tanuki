using System.Collections.Generic;
using Game.Common.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Goals.Utils
{
    public static class GoalsUtils
    {
        public static void Add([NotNull] this IGoals goals, [NotNull] IEnumerable<IGoal> entries)
        {
            ArgumentNullException.ThrowIfNull(goals);
            ArgumentNullException.ThrowIfNull(entries);

            foreach (IGoal goal in entries)
            {
                goals.Add(goal);
            }
        }

        public static bool AreCompleted([NotNull] this IGoals goals)
        {
            ArgumentNullException.ThrowIfNull(goals);

            foreach (IGoal goal in goals.Entries)
            {
                if (!goal.IsCompleted())
                {
                    return false;
                }
            }

            return true;
        }

        public static bool TryIncreaseCurrentAmount(
            [NotNull] this IGoals goals,
            PieceType pieceType,
            out int currentAmount)
        {
            ArgumentNullException.ThrowIfNull(goals);

            if (!goals.TryGet(pieceType, out IGoal goal))
            {
                currentAmount = -1;

                return false;
            }

            currentAmount = ++goal.CurrentAmount;

            return true;
        }
    }
}