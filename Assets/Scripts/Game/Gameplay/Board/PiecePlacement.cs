using Game.Gameplay.Board.Pieces;

namespace Game.Gameplay.Board
{
    public class PiecePlacement
    {
        public readonly int Row;
        public readonly int Column;
        public readonly IPiece Piece;

        public PiecePlacement(int row, int column, IPiece piece)
        {
            Row = row;
            Column = column;
            Piece = piece;
        }
    }
}