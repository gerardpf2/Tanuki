using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Pieces;
using Game.Gameplay.Pieces.Parsing;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Parsing
{
    public class BoardSerializedDataConverter : IBoardSerializedDataConverter
    {
        [NotNull] private readonly IPiecePlacementSerializedDataConverter _piecePlacementSerializedDataConverter;

        public BoardSerializedDataConverter(
            [NotNull] IPiecePlacementSerializedDataConverter piecePlacementSerializedDataConverter)
        {
            ArgumentNullException.ThrowIfNull(piecePlacementSerializedDataConverter);

            _piecePlacementSerializedDataConverter = piecePlacementSerializedDataConverter;
        }

        public void To(
            [NotNull] BoardSerializedData boardSerializedData,
            out IBoard board,
            out IEnumerable<PiecePlacement> piecePlacements)
        {
            ArgumentNullException.ThrowIfNull(boardSerializedData);
            ArgumentNullException.ThrowIfNull(boardSerializedData.PiecePlacementSerializedData);

            board = new Board(boardSerializedData.Columns);

            piecePlacements =
                boardSerializedData.PiecePlacementSerializedData
                    .Select(_piecePlacementSerializedDataConverter.To)
                    .ToList();
        }

        public BoardSerializedData From([NotNull] IBoard board)
        {
            ArgumentNullException.ThrowIfNull(board);

            return
                new BoardSerializedData
                {
                    Columns = board.Columns,
                    PiecePlacementSerializedData = board.PieceIds.Select(GetPiecePlacementSerializedData).ToList()
                };

            PiecePlacementSerializedData GetPiecePlacementSerializedData(int pieceId)
            {
                IPiece piece = board.GetPiece(pieceId);
                Coordinate sourceCoordinate = board.GetSourceCoordinate(pieceId);

                PiecePlacement piecePlacement = new(piece, sourceCoordinate);

                return _piecePlacementSerializedDataConverter.From(piecePlacement);
            }
        }
    }
}