using Game.Gameplay.Board;

namespace Game.Gameplay.Goals
{
    public class Goal : IGoal
    {
        public PieceType PieceType { get; }

        public int Amount { get; }

        public int Current { get; private set; }

        public void IncreaseCurrent()
        {
            ++Current;
        }

        public Goal(PieceType pieceType, int amount)
        {
            PieceType = pieceType;
            Amount = amount;
        }
    }
}