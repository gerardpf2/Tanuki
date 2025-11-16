using Game.Common.Pieces;

namespace Game.Gameplay.Pieces
{
    public interface IPieceTypeCategoryRegistry
    {
        PieceType GetDecomposeBlockType(PieceType pieceType);
    }
}