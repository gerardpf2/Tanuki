using Game.Gameplay.Board.Pieces;

namespace Game.Gameplay.Board
{
    public interface IBoard
    {
        int Rows { get; }

        int Columns { get; }

        void Add(IPiece piece, Coordinate sourceCoordinate);

        IPiece Get(Coordinate coordinate);

        void Remove(IPiece piece);

        void Move(IPiece piece, int rowOffset, int columnOffset);
    }
}