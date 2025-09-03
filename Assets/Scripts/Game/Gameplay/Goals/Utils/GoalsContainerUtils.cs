using System.Linq;
using Game.Gameplay.Board;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Goals.Utils
{
    public static class GoalsContainerUtils
    {
        public static bool AreAllCompleted([NotNull] this IGoalsContainer goalsContainer)
        {
            ArgumentNullException.ThrowIfNull(goalsContainer);

            return goalsContainer.PieceTypes.All(goalsContainer.IsCompleted);
        }

        private static bool IsCompleted([NotNull] this IGoalsContainer goalsContainer, PieceType pieceType)
        {
            ArgumentNullException.ThrowIfNull(goalsContainer);

            int initialAmount = goalsContainer.GetInitialAmount(pieceType);
            int currentAmount = goalsContainer.GetCurrentAmount(pieceType);

            return initialAmount <= currentAmount;
        }
    }
}