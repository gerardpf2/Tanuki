using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.Events.Events
{
    public class DestroyPieceEvent : IEvent
    {
        public readonly UpdateGoalEvent UpdateGoalEvent;
        public readonly int PieceId;
        public readonly DestroyPieceReason DestroyPieceReason;
        public readonly DecomposePieceData DecomposePieceData;

        public DestroyPieceEvent(
            UpdateGoalEvent updateGoalEvent,
            int pieceId,
            DestroyPieceReason destroyPieceReason,
            DecomposePieceData decomposePieceData)
        {
            UpdateGoalEvent = updateGoalEvent;
            PieceId = pieceId;
            DestroyPieceReason = destroyPieceReason;
            DecomposePieceData = decomposePieceData;
        }
    }
}