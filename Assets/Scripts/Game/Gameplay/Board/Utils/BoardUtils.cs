using System;
using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.Board.Pieces.Utils;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.Board.Utils
{
    public static class BoardUtils
    {
        public static bool IsInside([NotNull] this IBoard board, Coordinate coordinate)
        {
            ArgumentNullException.ThrowIfNull(board);

            return
                coordinate.Row >= 0 && coordinate.Row < board.Rows &&
                coordinate.Column >= 0 && coordinate.Column < board.Columns;
        }

        [NotNull]
        public static IEnumerable<int> GetPieceIdsSortedByRowThenByColumn([NotNull] this IBoard board)
        {
            ArgumentNullException.ThrowIfNull(board);

            return
                board.PieceIds
                    .Select(SelectPieceIdSourceCoordinate)
                    .OrderBy(SelectRow)
                    .ThenBy(SelectColumn)
                    .Select(SelectPieceId);

            (int, Coordinate) SelectPieceIdSourceCoordinate(int pieceId)
            {
                return (pieceId, board.GetSourceCoordinate(pieceId));
            }

            int SelectRow((int _, Coordinate sourceCoordinate) pieceIdSourceCoordinate)
            {
                return pieceIdSourceCoordinate.sourceCoordinate.Row;
            }

            int SelectColumn((int _, Coordinate sourceCoordinate) pieceIdSourceCoordinate)
            {
                return pieceIdSourceCoordinate.sourceCoordinate.Column;
            }

            int SelectPieceId((int pieceId, Coordinate _) pieceIdSourceCoordinate)
            {
                return pieceIdSourceCoordinate.pieceId;
            }
        }

        [NotNull]
        public static IEnumerable<KeyValuePair<int, int>> GetPieceIdsInRow([NotNull] this IBoard board, int row)
        {
            ArgumentNullException.ThrowIfNull(board);

            int columns = board.Columns;

            for (int column = 0; column < columns; ++column)
            {
                Coordinate coordinate = new(row, column);

                int? pieceId = board.GetPieceId(coordinate);

                if (!pieceId.HasValue)
                {
                    continue;
                }

                yield return new KeyValuePair<int, int>(pieceId.Value, column);
            }
        }

        public static void GetPieceRowColumnOffset(
            [NotNull] this IBoard board,
            int pieceId,
            int row,
            int column,
            out int rowOffset,
            out int columnOffset)
        {
            ArgumentNullException.ThrowIfNull(board);

            Coordinate sourceCoordinate = board.GetSourceCoordinate(pieceId);

            rowOffset = row - sourceCoordinate.Row;
            columnOffset = column - sourceCoordinate.Column;
        }

        public static int ComputePieceFall(
            [NotNull] this IBoard board,
            [NotNull] IPiece piece,
            Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(piece);

            int fall = int.MaxValue;

            foreach (Coordinate coordinate in piece.GetCoordinates(sourceCoordinate))
            {
                fall = Math.Min(board.ComputePieceFallImpl(piece.Id, coordinate), fall);
            }

            return fall;
        }

        private static int ComputePieceFallImpl([NotNull] this IBoard board, int ignorePieceId, Coordinate coordinate)
        {
            ArgumentNullException.ThrowIfNull(board);

            int rowsAboveBoard = Math.Max(coordinate.Row - board.Rows, 0);
            int fall = rowsAboveBoard;

            for (int row = coordinate.Row - rowsAboveBoard - 1; row >= 0; --row)
            {
                Coordinate coordinateBelow = new(row, coordinate.Column);

                int? pieceId = board.GetPieceId(coordinateBelow);

                if (pieceId.HasValue && pieceId != ignorePieceId)
                {
                    break;
                }

                ++fall;
            }

            return fall;
        }
    }
}