using Game.Gameplay.Board.Pieces;

namespace Game.Gameplay.Board
{
    public interface IPieceCachedPropertiesGetter
    {
        int GetTopMostRowOffset(IPiece piece);

        int GetRightMostColumnOffset(IPiece piece);
    }
}