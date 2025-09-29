using Game.Gameplay.Board;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Goals.Utils
{
    public static class GoalsUtils
    {
        public static bool AreCompleted([NotNull] this IGoals goals)
        {
            ArgumentNullException.ThrowIfNull(goals);

            foreach (IGoal goal in goals.Targets)
            {
                if (!goal.IsCompleted())
                {
                    return false;
                }
            }

            return true;
        }

        public static bool TryIncreaseCurrentAmount([NotNull] this IGoals goals, PieceType pieceType)
        {
            ArgumentNullException.ThrowIfNull(goals);

            if (!goals.TryGet(pieceType, out IGoal goal))
            {
                return false;
            }

            goal.IncreaseCurrentAmount();

            return true;
        }
    }
}