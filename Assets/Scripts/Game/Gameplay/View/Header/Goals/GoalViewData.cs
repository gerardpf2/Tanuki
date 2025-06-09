using Game.Gameplay.Board;

namespace Game.Gameplay.View.Header.Goals
{
    public class GoalViewData
    {
        public readonly PieceType PieceType;
        public readonly int InitialAmount;
        public readonly int CurrentAmount;

        public GoalViewData(PieceType pieceType, int initialAmount, int currentAmount)
        {
            PieceType = pieceType;
            InitialAmount = initialAmount;
            CurrentAmount = currentAmount;
        }

        // TODO: Equals
    }
}