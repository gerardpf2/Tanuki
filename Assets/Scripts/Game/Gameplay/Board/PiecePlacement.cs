using Game.Gameplay.Board.Pieces;

namespace Game.Gameplay.Board
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