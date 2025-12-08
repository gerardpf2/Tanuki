using System;
using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.Board.Utils
{
    public static class BoardUtils
    {
        [ContractAnnotation("=> true, piece:notnull; => false, piece:null")]
        public static bool TryGetPiece([NotNull] this IBoard board, Coordinate coordinate, out IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(board);

            int? pieceId = board.GetPieceId(coordinate);

            if (!pieceId.HasValue)
            {
                piece = null;

                return false;
            }

            piece = board.GetPiece(pieceId.Value);

            return true;
        }

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

            for (int row = bottomRow; row <= topRow; ++row)
            {
                foreach (int pieceId in board.GetPieceIdsInRow(row))
                {
                    if (visitedPieceIds.Contains(pieceId))
                    {
                        continue;
                    }

                    visitedPieceIds.Add(pieceId);

                    yield return pieceId;
                }
            }
        }

        public static bool IsRowFull([NotNull] this IBoard board, int row)
        {
            ArgumentNullException.ThrowIfNull(board);

            return board.GetPieceIdsInRow(row).Count() == board.Columns;
        }

        [NotNull]
        public static IEnumerable<int> GetPieceIdsInRow([NotNull] this IBoard board, int row)
        {
            ArgumentNullException.ThrowIfNull(board);

            foreach (Coordinate coordinate in board.GetCoordinatesInRow(row))
            {
                if (!board.TryGetPiece(coordinate, out IPiece piece))
                {
                    continue;
                }

                yield return piece.Id;
            }
        }

        [NotNull]
        public static IEnumerable<Coordinate> GetCoordinatesInRow([NotNull] this IBoard board, int row)
        {
            ArgumentNullException.ThrowIfNull(board);

            int columns = board.Columns;

            for (int column = 0; column < columns; ++column)
            {
                Coordinate coordinate = new(row, column);

                yield return coordinate;
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

                if (board.TryGetPiece(coordinateBelow, out IPiece piece) && piece.Id != ignorePieceId)
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

                if (!board.TryGetPiece(otherCoordinate, out IPiece otherPiece))
                {
                    continue;
                }

                int otherPieceId = otherPiece.Id;

                if (otherPiece == piece || visitedPieceIds.Contains(otherPieceId))
                {
                    continue;
                }

                visitedPieceIds.Add(otherPieceId);

                yield return otherPieceId;
            }
        }
    }
}