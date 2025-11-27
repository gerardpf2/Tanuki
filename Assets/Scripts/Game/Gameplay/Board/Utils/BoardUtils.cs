using System;
using System.Collections.Generic;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.Board.Utils
{
    public static class BoardUtils
    {
        [NotNull]
        public static IEnumerable<int> GetDistinctPieceIdsSortedByRowThenByColumn([NotNull] this IBoard board)
        {
            ArgumentNullException.ThrowIfNull(board);

            const int bottomRow = 0;
            int topRow = board.HighestNonEmptyRow;

            return board.GetDistinctPieceIdsSortedByRowThenByColumn(bottomRow, topRow);
        }

        [NotNull]
        public static IEnumerable<int> GetDistinctPieceIdsSortedByRowThenByColumn(
            [NotNull] this IBoard board,
            int bottomRow,
            int topRow)
        {
            ArgumentNullException.ThrowIfNull(board);

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

        public static Coordinate GetPieceLockSourceCoordinate(
            [NotNull] this IBoard board,
            [NotNull] IPiece piece,
            Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(piece);

            int fall = board.ComputePieceFall(piece, sourceCoordinate);

            Coordinate lockSourceCoordinate = sourceCoordinate.Down(fall);

            return lockSourceCoordinate;
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

                if (fall <= 0)
                {
                    break;
                }
            }

            return fall;
        }

        [NotNull]
        public static IEnumerable<int> GetDistinctPieceIdsInContactDown(
            [NotNull] this IBoard board,
            [NotNull] IPiece piece,
            Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(piece);

            return board.GetDistinctPieceIdsInContact(piece, sourceCoordinate, OtherCoordinateGetter);

            Coordinate OtherCoordinateGetter(Coordinate coordinate)
            {
                return coordinate.Down();
            }
        }

        [NotNull]
        public static IEnumerable<int> GetDistinctPieceIdsInContactLeft(
            [NotNull] this IBoard board,
            [NotNull] IPiece piece,
            Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(piece);

            return board.GetDistinctPieceIdsInContact(piece, sourceCoordinate, OtherCoordinateGetter);

            Coordinate OtherCoordinateGetter(Coordinate coordinate)
            {
                return coordinate.Left();
            }
        }

        [NotNull]
        public static IEnumerable<int> GetDistinctPieceIdsInContactRight(
            [NotNull] this IBoard board,
            [NotNull] IPiece piece,
            Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(piece);

            return board.GetDistinctPieceIdsInContact(piece, sourceCoordinate, OtherCoordinateGetter);

            Coordinate OtherCoordinateGetter(Coordinate coordinate)
            {
                return coordinate.Right();
            }
        }

        private static int ComputePieceFallImpl([NotNull] this IBoard board, int ignorePieceId, Coordinate coordinate)
        {
            ArgumentNullException.ThrowIfNull(board);

            int rowsAboveBoard = Math.Max(coordinate.Row - board.HighestNonEmptyRow - 1, 0);
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

        [NotNull]
        private static IEnumerable<int> GetDistinctPieceIdsInContact(
            [NotNull] this IBoard board,
            [NotNull] IPiece piece,
            Coordinate sourceCoordinate,
            [NotNull] Func<Coordinate, Coordinate> otherCoordinateGetter)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(piece);
            ArgumentNullException.ThrowIfNull(otherCoordinateGetter);

            ICollection<int> visitedPieceIds = new HashSet<int>();

            foreach (Coordinate coordinate in piece.GetCoordinates(sourceCoordinate))
            {
                Coordinate otherCoordinate = otherCoordinateGetter(coordinate);

                if (!board.IsInside(otherCoordinate))
                {
                    continue;
                }

                int? otherPieceId = board.GetPieceId(otherCoordinate);

                if (!otherPieceId.HasValue)
                {
                    continue;
                }

                int otherPieceIdValue = otherPieceId.Value;

                if (otherPieceIdValue == piece.Id || visitedPieceIds.Contains(otherPieceIdValue))
                {
                    continue;
                }

                visitedPieceIds.Add(otherPieceIdValue);

                yield return otherPieceIdValue;
            }
        }
    }
}