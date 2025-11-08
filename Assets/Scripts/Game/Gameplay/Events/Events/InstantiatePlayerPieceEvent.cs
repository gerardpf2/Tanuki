using Game.Gameplay.Board;
using Game.Gameplay.Pieces.Pieces;

namespace Game.Gameplay.Events.Events
{
    public class InstantiatePlayerPieceEvent : IEvent
    {
        public readonly IPiece Piece;
        public readonly Coordinate SourceCoordinate;

        public InstantiatePlayerPieceEvent(IPiece piece, Coordinate sourceCoordinate)
        {
            Piece = piece;
            SourceCoordinate = sourceCoordinate;
        }
    }
}