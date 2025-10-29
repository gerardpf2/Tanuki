using Game.Gameplay.Board;
using Game.Gameplay.Pieces.Pieces;

namespace Game.Gameplay.Pieces
{
    public class PiecePlacement
    {
        public readonly IPiece Piece;
        public readonly Coordinate Coordinate;

        public PiecePlacement(IPiece piece, Coordinate coordinate)
        {
            Piece = piece;
            Coordinate = coordinate;
        }
    }
}