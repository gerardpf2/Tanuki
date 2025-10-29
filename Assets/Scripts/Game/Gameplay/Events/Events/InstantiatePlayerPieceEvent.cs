using Game.Gameplay.Pieces.Pieces;

namespace Game.Gameplay.Events.Events
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