using Game.Gameplay.Board.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Parsing
{
    public interface IPieceSerializedDataConverter
    {
        [NotNull]
        IPiece To(PieceSerializedData pieceSerializedData);

        [NotNull]
        PieceSerializedData From(IPiece piece);
    }
}