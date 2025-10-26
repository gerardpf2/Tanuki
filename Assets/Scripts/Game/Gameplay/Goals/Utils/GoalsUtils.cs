using Game.Common.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Goals.Utils
{
    public static class GoalsUtils
    {
        public static bool AreCompleted([NotNull] this IGoals goals)
        {
            ArgumentNullException.ThrowIfNull(goals);

            foreach (PieceType pieceType in goals.PieceTypes)
            {
                IGoal goal = goals.Get(pieceType);

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