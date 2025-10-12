using Game.Gameplay.EventEnqueueing.Events.Reasons;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class DestroyPieceEvent : IEvent
    {
        public readonly int PieceId;
        public readonly DestroyPieceReason DestroyPieceReason;

        public DestroyPieceEvent(int pieceId, DestroyPieceReason destroyPieceReason)
        {
            PieceId = pieceId;
            DestroyPieceReason = destroyPieceReason;
        }
    }
}