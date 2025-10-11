using Game.Gameplay.Board;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class SetGoalCurrentAmountEvent : IEvent
    {
        public readonly PieceType PieceType;
        public readonly int CurrentAmount;

        public SetGoalCurrentAmountEvent(PieceType pieceType, int currentAmount)
        {
            PieceType = pieceType;
            CurrentAmount = currentAmount;
        }
    }
}