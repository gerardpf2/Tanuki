using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Board.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Parsing
{
    public class BoardSerializedDataConverter : IBoardSerializedDataConverter
    {
        [NotNull] private readonly IPieceCachedPropertiesGetter _pieceCachedPropertiesGetter;
        [NotNull] private readonly IPiecePlacementSerializedDataConverter _piecePlacementSerializedDataConverter;

        public BoardSerializedDataConverter(
            [NotNull] IPieceCachedPropertiesGetter pieceCachedPropertiesGetter,
            [NotNull] IPiecePlacementSerializedDataConverter piecePlacementSerializedDataConverter)
        {
            ArgumentNullException.ThrowIfNull(pieceCachedPropertiesGetter);
            ArgumentNullException.ThrowIfNull(piecePlacementSerializedDataConverter);

            _pieceCachedPropertiesGetter = pieceCachedPropertiesGetter;
            _piecePlacementSerializedDataConverter = piecePlacementSerializedDataConverter;
        }

        public void To(
            [NotNull] BoardSerializedData boardSerializedData,
            out IBoard board,
            out IEnumerable<PiecePlacement> piecePlacements)
        {
            ArgumentNullException.ThrowIfNull(boardSerializedData);
            ArgumentNullException.ThrowIfNull(boardSerializedData.PiecePlacementSerializedData);

            int rows = boardSerializedData.Rows;
            int columns = boardSerializedData.Columns;

            board = new Board(_pieceCachedPropertiesGetter, rows, columns);

            piecePlacements =
                boardSerializedData.PiecePlacementSerializedData
                    .Select(_piecePlacementSerializedDataConverter.To)
                    .ToList();
        }

        public BoardSerializedData From([NotNull] IReadonlyBoard board)
        {
            ArgumentNullException.ThrowIfNull(board);

            return
                new BoardSerializedData
                {
                    Rows = board.Rows,
                    Columns = board.Columns,
                    PiecePlacementSerializedData = board.PieceSourceCoordinates.Select(Convert).ToList()
                };

            PiecePlacementSerializedData Convert(KeyValuePair<IPiece, Coordinate> pieceSourceCoordinate)
            {
                IPiece piece = pieceSourceCoordinate.Key;
                Coordinate sourceCoordinate = pieceSourceCoordinate.Value;

                PiecePlacement piecePlacement = new(sourceCoordinate.Row, sourceCoordinate.Column, piece);

                return _piecePlacementSerializedDataConverter.From(piecePlacement);
            }
        }
    }
}