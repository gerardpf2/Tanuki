using Game.Gameplay.Board;

namespace Game.Gameplay.Goals
{
    public class Goal : IGoal
    {
        public PieceType PieceType { get; }

        public int Amount { get; }

        public Goal(PieceType pieceType, int amount)
        {
            PieceType = pieceType;
            Amount = amount;
        }
    }
}