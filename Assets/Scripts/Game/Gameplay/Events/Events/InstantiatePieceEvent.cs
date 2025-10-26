using Game.Gameplay.Board;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;

namespace Game.Gameplay.Events.Events
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