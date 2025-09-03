using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events.Reasons;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class InstantiateEvent : IEvent
    {
        public readonly IPiece Piece;
        public readonly PieceType PieceType;
        public readonly Coordinate SourceCoordinate;
        public readonly InstantiateReason InstantiateReason;

        public InstantiateEvent(
            IPiece piece,
            PieceType pieceType,
            Coordinate sourceCoordinate,
            InstantiateReason instantiateReason)
        {
            Piece = piece;
            PieceType = pieceType;
            SourceCoordinate = sourceCoordinate;
            InstantiateReason = instantiateReason;
        }
    }
}