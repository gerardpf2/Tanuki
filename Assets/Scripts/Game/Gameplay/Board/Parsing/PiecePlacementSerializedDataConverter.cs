using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Parsing
{
    public class PiecePlacementSerializedDataConverter : IPiecePlacementSerializedDataConverter
    {
        [NotNull] private readonly IPieceSerializedDataConverter _pieceSerializedDataConverter;

        public PiecePlacementSerializedDataConverter([NotNull] IPieceSerializedDataConverter pieceSerializedDataConverter)
        {
            ArgumentNullException.ThrowIfNull(pieceSerializedDataConverter);

            _pieceSerializedDataConverter = pieceSerializedDataConverter;
        }

        public PiecePlacement To([NotNull] PiecePlacementSerializedData piecePlacementSerializedData)
        {
            ArgumentNullException.ThrowIfNull(piecePlacementSerializedData);

            return
                new PiecePlacement(
                    piecePlacementSerializedData.Row,
                    piecePlacementSerializedData.Column,
                    _pieceSerializedDataConverter.To(piecePlacementSerializedData.PieceSerializedData)
                );
        }

        public PiecePlacementSerializedData From([NotNull] PiecePlacement piecePlacement)
        {
            ArgumentNullException.ThrowIfNull(piecePlacement);

            return
                new PiecePlacementSerializedData
                {
                    Row = piecePlacement.Row,
                    Column = piecePlacement.Column,
                    PieceSerializedData = _pieceSerializedDataConverter.From(piecePlacement.Piece)
                };
        }
    }
}