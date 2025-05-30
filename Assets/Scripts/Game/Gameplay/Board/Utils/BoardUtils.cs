using System;
using System.Collections.Generic;
using Game.Gameplay.Board.Pieces;
using Infrastructure.System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.Board.Utils
{
    public static class BoardUtils
    {
        public static bool IsInside([NotNull] this IReadonlyBoard board, Coordinate coordinate)
        {
            ArgumentNullException.ThrowIfNull(board);

            return
                coordinate.Row >= 0 && coordinate.Row < board.Rows &&
                coordinate.Column >= 0 && coordinate.Column < board.Columns;
        }

        [NotNull, ItemNotNull]
        public static IEnumerable<PiecePlacement> GetAllPieces([NotNull] this IReadonlyBoard board)
        {
            ArgumentNullException.ThrowIfNull(board);

            return board.GetPiecesInRange(0, board.Rows - 1, 0, board.Columns - 1);
        }

        [NotNull, ItemNotNull]
        public static IEnumerable<PiecePlacement> GetPiecesInRow([NotNull] this IReadonlyBoard board, int row)
        {
            ArgumentNullException.ThrowIfNull(board);

            return board.GetPiecesInRange(row, row, 0, board.Columns - 1);
        }

        public static bool IsRowFull([NotNull] this IReadonlyBoard board, int row)
        {
            ArgumentNullException.ThrowIfNull(board);

            int columns = board.Columns;

            for (int column = 0; column < columns; ++column)
            {
                Coordinate coordinate = new(row, column);

                IPiece piece = board.Get(coordinate);

                if (piece is null)
                {
                    return false;
                }
            }

            return true;
        }

        public static Coordinate GetPieceSourceCoordinate([NotNull] this IReadonlyBoard board, [NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(piece);

            if (!board.PieceSourceCoordinates.TryGetValue(piece, out Coordinate sourceCoordinate))
            {
                InvalidOperationException.Throw("Piece cannot be found");
            }

            return sourceCoordinate;
        }

        public static void GetPieceRowColumnOffset(
            [NotNull] this IReadonlyBoard board,
            [NotNull] IPiece piece,
            int row,
            int column,
            out int rowOffset,
            out int columnOffset)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(piece);

            Coordinate sourceCoordinate = board.GetPieceSourceCoordinate(piece);

            rowOffset = row - sourceCoordinate.Row;
            columnOffset = column - sourceCoordinate.Column;
        }

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0)]
        public static int ComputePieceFall(
            [NotNull] this IReadonlyBoard board,
            [NotNull] IPiece piece,
            Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(piece);

            int fall = int.MaxValue;

            foreach (Coordinate coordinate in piece.GetCoordinates(sourceCoordinate))
            {
                fall = Math.Min(board.ComputePieceFallImpl(piece, coordinate), fall);
            }

            return fall;
        }

        [NotNull, ItemNotNull]
        private static IEnumerable<PiecePlacement> GetPiecesInRange(
            [NotNull] this IReadonlyBoard board,
            int rowStart,
            int rowEnd,
            int columnStart,
            int columnEnd)
        {
            ArgumentNullException.ThrowIfNull(board);

            ICollection<IPiece> visitedPieces = new HashSet<IPiece>();

            for (int row = rowStart; row <= rowEnd; ++row)
            {
                for (int column = columnStart; column <= columnEnd; ++column)
                {
                    Coordinate coordinate = new(row, column);

                    IPiece piece = board.Get(coordinate);

                    if (piece is null || visitedPieces.Contains(piece))
                    {
                        continue;
                    }

                    visitedPieces.Add(piece);

                    PiecePlacement piecePlacement = new(row, column, piece);

                    yield return piecePlacement;
                }
            }
        }

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0)]
        private static int ComputePieceFallImpl(
            [NotNull] this IReadonlyBoard board,
            IPiece ignorePiece,
            Coordinate coordinate)
        {
            ArgumentNullException.ThrowIfNull(board);

            int rowsAboveBoard = Math.Max(coordinate.Row - board.Rows, 0);
            int fall = rowsAboveBoard;

            for (int row = coordinate.Row - rowsAboveBoard - 1; row >= 0; --row)
            {
                IPiece piece = board.Get(new Coordinate(row, coordinate.Column));

                if (piece is not null && piece != ignorePiece)
                {
                    break;
                }

                ++fall;
            }

            return fall;
        }
    }
}