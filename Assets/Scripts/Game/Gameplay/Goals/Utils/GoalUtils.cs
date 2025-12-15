using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Goals.Utils
{
    public static class GoalUtils
    {
        public static bool IsCompleted([NotNull] this IGoal goal)
        {
            ArgumentNullException.ThrowIfNull(goal);

            return goal.InitialAmount <= goal.CurrentAmount;
        }
    }
}