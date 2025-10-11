using Game.Gameplay.Board;
using Game.Gameplay.EventEnqueueing.Events.Reasons;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class DestroyPieceEvent : IEvent
    {
        public readonly int PieceId;
        public readonly PieceType PieceType;
        public readonly DestroyPieceReason DestroyPieceReason;

        public DestroyPieceEvent(int pieceId, PieceType pieceType, DestroyPieceReason destroyPieceReason)
        {
            PieceId = pieceId;
            PieceType = pieceType;
            DestroyPieceReason = destroyPieceReason;
        }
    }
}