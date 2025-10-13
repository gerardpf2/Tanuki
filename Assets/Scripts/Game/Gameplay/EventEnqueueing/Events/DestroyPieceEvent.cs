using Game.Gameplay.Board;
using Game.Gameplay.EventEnqueueing.Events.Reasons;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class DestroyPieceEvent : IEvent
    {
        public class GoalCurrentAmountUpdatedData
        {
            public readonly PieceType PieceType;
            public readonly int CurrentAmount;
            public readonly Coordinate Coordinate;

            public GoalCurrentAmountUpdatedData(PieceType pieceType, int currentAmount, Coordinate coordinate)
            {
                PieceType = pieceType;
                CurrentAmount = currentAmount;
                Coordinate = coordinate;
            }
        }

        public readonly int PieceId;
        public readonly DestroyPieceReason DestroyPieceReason;
        public readonly GoalCurrentAmountUpdatedData GoalData;

        public DestroyPieceEvent(
            int pieceId,
            DestroyPieceReason destroyPieceReason,
            GoalCurrentAmountUpdatedData goalData)
        {
            PieceId = pieceId;
            DestroyPieceReason = destroyPieceReason;
            GoalData = goalData;
        }
    }
}