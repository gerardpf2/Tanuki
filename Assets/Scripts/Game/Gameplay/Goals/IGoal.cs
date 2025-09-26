using Game.Gameplay.Board;

namespace Game.Gameplay.Goals
{
    public interface IGoal
    {
        PieceType PieceType { get; }

        int InitialAmount { get; }

        int CurrentAmount { get; }

        void IncreaseCurrentAmount();
    }
}