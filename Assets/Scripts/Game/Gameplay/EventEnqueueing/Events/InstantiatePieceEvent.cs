using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events.Reasons;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class InstantiatePieceEvent : IEvent
    {
        public readonly IPiece Piece;
        public readonly Coordinate SourceCoordinate;
        public readonly InstantiatePieceReason InstantiatePieceReason;

        public InstantiatePieceEvent(
            IPiece piece,
            Coordinate sourceCoordinate,
            InstantiatePieceReason instantiatePieceReason)
        {
            Piece = piece;
            SourceCoordinate = sourceCoordinate;
            InstantiatePieceReason = instantiatePieceReason;
        }
    }
}