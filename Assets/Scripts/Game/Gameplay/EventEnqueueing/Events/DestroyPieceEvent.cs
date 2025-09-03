using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events.Reasons;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class DestroyPieceEvent : IEvent
    {
        public readonly IPiece Piece;
        public readonly DestroyPieceReason DestroyPieceReason;

        public DestroyPieceEvent(IPiece piece, DestroyPieceReason destroyPieceReason)
        {
            Piece = piece;
            DestroyPieceReason = destroyPieceReason;
        }
    }
}