using System;
using System.Collections.Generic;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;

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
        public static IEnumerable<int> GetDistinctPieceIdsSortedByRowThenByColumn([NotNull] this IBoard board)
        {
            ArgumentNullException.ThrowIfNull(board);

            const int bottomRow = 0;
            int topRow = board.Rows - 1;

            return board.GetDistinctPieceIdsSortedByRowThenByColumn(bottomRow, topRow);
        }

        [NotNull]
        public static IEnumerable<int> GetDistinctPieceIdsSortedByRowThenByColumn(
            [NotNull] this IBoard board,
            int bottomRow,
            int topRow)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentOutOfRangeException.ThrowIfNot(bottomRow, ComparisonOperator.GreaterThanOrEqualTo, 0);
            ArgumentOutOfRangeException.ThrowIfNot(topRow, ComparisonOperator.LessThan, board.Rows);

            ICollection<int> visitedPieceIds = new HashSet<int>();

            int columns = board.Columns;

            for (int row = bottomRow; row <= topRow; ++row)
            {
                for (int column = 0; column < columns; ++column)
                {
                    Coordinate coordinate = new(row, column);

                    int? pieceId = board.GetPieceId(coordinate);

                    if (!pieceId.HasValue)
                    {
                        continue;
                    }

                    int pieceIdValue = pieceId.Value;

                    if (visitedPieceIds.Contains(pieceIdValue))
                    {
                        continue;
                    }

                    visitedPieceIds.Add(pieceIdValue);

                    yield return pieceIdValue;
                }
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