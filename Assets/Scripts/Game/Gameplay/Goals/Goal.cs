using Game.Gameplay.Board;

namespace Game.Gameplay.Goals
{
    public class Goal : IGoal
    {
        public PieceType PieceType { get; }

        public int InitialAmount { get; }

        public int CurrentAmount { get; set; }

        public Goal(PieceType pieceType, int initialAmount)
        {
            PieceType = pieceType;
            InitialAmount = initialAmount;
        }

        public IGoal Clone()
        {
            return new Goal(PieceType, InitialAmount) { CurrentAmount = CurrentAmount };
        }
    }
}