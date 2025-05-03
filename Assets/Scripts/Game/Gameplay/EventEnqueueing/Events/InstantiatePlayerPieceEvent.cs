using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class InstantiatePlayerPieceEvent : IEvent
    {
        public readonly IPiece Piece;
        public readonly PieceType PieceType;

        public InstantiatePlayerPieceEvent(IPiece piece, PieceType pieceType)
        {
            Piece = piece;
            PieceType = pieceType;
        }
    }
}