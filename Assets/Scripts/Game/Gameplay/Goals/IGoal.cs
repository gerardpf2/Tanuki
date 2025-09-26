using Game.Gameplay.Board;
using JetBrains.Annotations;

namespace Game.Gameplay.Goals
{
    public interface IGoal
    {
        PieceType PieceType { get; }

        int InitialAmount { get; }

        int CurrentAmount { get; }

        void IncreaseCurrentAmount();

        [NotNull]
        IGoal Clone();
    }
}