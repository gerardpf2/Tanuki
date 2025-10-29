using Game.Gameplay.Pieces.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Parsing
{
    public interface IPieceSerializedDataConverter
    {
        [NotNull]
        IPiece To(PieceSerializedData pieceSerializedData);

        [NotNull]
        PieceSerializedData From(IPiece piece);
    }
}