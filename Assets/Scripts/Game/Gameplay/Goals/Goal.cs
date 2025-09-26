using Game.Gameplay.Board;

namespace Game.Gameplay.Goals
{
    public class Goal : IGoal
    {
        public PieceType PieceType { get; }

        public int InitialAmount { get; }

        public int CurrentAmount { get; private set; }

        public void IncreaseCurrentAmount()
        {
            ++CurrentAmount;
        }

        public Goal(PieceType pieceType, int initialAmount)
        {
            PieceType = pieceType;
            InitialAmount = initialAmount;
        }
    }
}