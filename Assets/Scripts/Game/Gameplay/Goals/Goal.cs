using Game.Gameplay.Board;
using Game.Gameplay.Pieces;

namespace Game.Gameplay.Goals
{
    public class Goal : IGoal
    {
        public PieceType PieceType { get; }

        public int InitialAmount { get; }

        public int CurrentAmount { get; set; }

        public Goal(PieceType pieceType, int initialAmount, int currentAmount)
        {
            PieceType = pieceType;
            InitialAmount = initialAmount;
            CurrentAmount = currentAmount;
        }

        public IGoal Clone()
        {
            return new Goal(PieceType, InitialAmount, CurrentAmount);
        }
    }
}