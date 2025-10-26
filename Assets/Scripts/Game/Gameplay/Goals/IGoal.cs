using Game.Gameplay.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.Goals
{
    public interface IGoal
    {
        PieceType PieceType { get; }

        int InitialAmount { get; }

        int CurrentAmount { get; set; }

        [NotNull]
        IGoal Clone();
    }
}