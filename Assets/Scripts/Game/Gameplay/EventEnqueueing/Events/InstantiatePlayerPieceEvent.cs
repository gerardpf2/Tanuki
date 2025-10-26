using Game.Gameplay.Pieces.Pieces;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class InstantiatePlayerPieceEvent : IEvent
    {
        public readonly IPiece Piece;

        public InstantiatePlayerPieceEvent(IPiece piece)
        {
            Piece = piece;
        }
    }
}