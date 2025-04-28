using Game.Gameplay.Model.Board.Pieces;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class InstantiateEvent : IEvent
    {
        public readonly IPiece Piece;
        public readonly PieceType PieceType;
        public readonly Coordinate SourceCoordinate;
        // TODO: InstantiateReason

        public InstantiateEvent(IPiece piece, PieceType pieceType, Coordinate sourceCoordinate)
        {
            Piece = piece;
            PieceType = pieceType;
            SourceCoordinate = sourceCoordinate;
        }
    }
}