using Game.Gameplay.Board;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Parsing
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
                    _pieceSerializedDataConverter.To(piecePlacementSerializedData.PieceSerializedData),
                    new Coordinate(piecePlacementSerializedData.Row, piecePlacementSerializedData.Column)
                );
        }

        public PiecePlacementSerializedData From([NotNull] PiecePlacement piecePlacement)
        {
            ArgumentNullException.ThrowIfNull(piecePlacement);

            return
                new PiecePlacementSerializedData
                {
                    Row = piecePlacement.Coordinate.Row,
                    Column = piecePlacement.Coordinate.Column,
                    PieceSerializedData = _pieceSerializedDataConverter.From(piecePlacement.Piece)
                };
        }
    }
}