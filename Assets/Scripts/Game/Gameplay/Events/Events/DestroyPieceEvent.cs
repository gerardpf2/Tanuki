using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.Events.Events
{
    public class DestroyPieceEvent : IEvent
    {
        public readonly int PieceId;
        public readonly DestroyPieceReason DestroyPieceReason;
        public readonly UpdateGoalData UpdateGoalData;
        public readonly DecomposePieceData DecomposePieceData;

        public DestroyPieceEvent(
            int pieceId,
            DestroyPieceReason destroyPieceReason,
            UpdateGoalData updateGoalData,
            DecomposePieceData decomposePieceData)
        {
            PieceId = pieceId;
            DestroyPieceReason = destroyPieceReason;
            UpdateGoalData = updateGoalData;
            DecomposePieceData = decomposePieceData;
        }
    }
}