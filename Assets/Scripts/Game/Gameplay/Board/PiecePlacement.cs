using Game.Gameplay.Board.Pieces;

namespace Game.Gameplay.Board
{
    public class PiecePlacement : IPiecePlacement
    {
        public int Row { get; }

        public int Column { get; }

        public IPiece Piece { get; }

        public PiecePlacement(int row, int column, IPiece piece)
        {
            Row = row;
            Column = column;
            Piece = piece;
        }
    }
}