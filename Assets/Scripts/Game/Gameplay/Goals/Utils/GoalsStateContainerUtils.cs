using System.Linq;
using Game.Gameplay.Board;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Goals.Utils
{
    public static class GoalsStateContainerUtils
    {
        public static bool IsCompleted([NotNull] this IGoalsStateContainer goalsStateContainer, PieceType pieceType)
        {
            ArgumentNullException.ThrowIfNull(goalsStateContainer);

            int initialAmount = goalsStateContainer.GetInitialAmount(pieceType);
            int currentAmount = goalsStateContainer.GetCurrentAmount(pieceType);

            return initialAmount <= currentAmount;
        }

        public static bool AreAllCompleted([NotNull] this IGoalsStateContainer goalsStateContainer)
        {
            ArgumentNullException.ThrowIfNull(goalsStateContainer);

            return goalsStateContainer.PieceTypes.All(goalsStateContainer.IsCompleted);
        }
    }
}