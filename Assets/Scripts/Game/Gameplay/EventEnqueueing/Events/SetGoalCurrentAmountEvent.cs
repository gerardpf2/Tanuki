using Game.Gameplay.Board;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class SetGoalCurrentAmountEvent : IEvent
    {
        public readonly PieceType PieceType;
        public readonly int CurrentAmount;
        public readonly Coordinate Coordinate;

        public SetGoalCurrentAmountEvent(PieceType pieceType, int currentAmount, Coordinate coordinate)
        {
            PieceType = pieceType;
            CurrentAmount = currentAmount;
            Coordinate = coordinate;
        }
    }
}