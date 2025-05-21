using Game.Gameplay.Board.Pieces;

namespace Game.Gameplay.Board
{
    // TODO: Remove
    public interface IPiecePlacement
    {
        int Row { get; }

        int Column { get; }

        IPiece Piece { get; }
    }
}