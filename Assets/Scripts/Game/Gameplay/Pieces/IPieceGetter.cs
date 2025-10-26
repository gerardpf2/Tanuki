using Game.Gameplay.Pieces.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces
{
    public interface IPieceGetter
    {
        [NotNull]
        IPiece Get(PieceType pieceType);
    }
}