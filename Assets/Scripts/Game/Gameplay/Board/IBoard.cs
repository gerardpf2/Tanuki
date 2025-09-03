using Game.Gameplay.Board.Pieces;

namespace Game.Gameplay.Board
{
    public interface IBoard : IReadonlyBoard
    {
        void Add(IPiece piece, Coordinate sourceCoordinate);

        void Remove(IPiece piece);

        void Move(IPiece piece, int rowOffset, int columnOffset);
    }
}