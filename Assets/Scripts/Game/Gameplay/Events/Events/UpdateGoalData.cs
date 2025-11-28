using Game.Common.Pieces;
using Game.Gameplay.Board;

namespace Game.Gameplay.Events.Events
{
    public class UpdateGoalData
    {
        public readonly PieceType PieceType;
        public readonly int CurrentAmount;
        public readonly Coordinate Coordinate;

        public UpdateGoalData(PieceType pieceType, int currentAmount, Coordinate coordinate)
        {
            PieceType = pieceType;
            CurrentAmount = currentAmount;
            Coordinate = coordinate;
        }
    }
}