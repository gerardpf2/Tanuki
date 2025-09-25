using System;
using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Board.Pieces;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

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

        [NotNull, ItemNotNull] // Distinct pieces
        public static IEnumerable<IPiece> GetPiecesSortedByRowThenByColumn([NotNull] this IBoard board)
        {
            ArgumentNullException.ThrowIfNull(board);

            return board.PieceSourceCoordinates.OrderBy(SelectRow).ThenBy(SelectColumn).Select(SelectPiece);

            int SelectRow(KeyValuePair<IPiece, Coordinate> pieceSourceCoordinate)
            {
                return pieceSourceCoordinate.Value.Row;
            }

            int SelectColumn(KeyValuePair<IPiece, Coordinate> pieceSourceCoordinate)
            {
                return pieceSourceCoordinate.Value.Column;
            }

            [NotNull]
            IPiece SelectPiece(KeyValuePair<IPiece, Coordinate> pieceSourceCoordinate)
            {
                IPiece piece = pieceSourceCoordinate.Key;

                InvalidOperationException.ThrowIfNull(piece);

                return piece;
            }
        }

        [NotNull]
        public static IEnumerable<KeyValuePair<IPiece, int>> GetPiecesInRow([NotNull] this IBoard board, int row)
        {
            ArgumentNullException.ThrowIfNull(board);

            int columns = board.Columns;

            for (int column = 0; column < columns; ++column)
            {
                Coordinate coordinate = new(row, column);

                IPiece piece = board.Get(coordinate);

                if (piece is null)
                {
                    continue;
                }

                yield return new KeyValuePair<IPiece, int>(piece, column);
            }
        }

        public static Coordinate GetPieceSourceCoordinate([NotNull] this IBoard board, [NotNull] IPiece piece)
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
            [NotNull] this IBoard board,
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
                fall = Math.Min(board.ComputePieceFallImpl(piece, coordinate), fall);
            }

            return fall;
        }

        private static int ComputePieceFallImpl([NotNull] this IBoard board, IPiece ignorePiece, Coordinate coordinate)
        {
            ArgumentNullException.ThrowIfNull(board);

            int rowsAboveBoard = Math.Max(coordinate.Row - board.Rows, 0);
            int fall = rowsAboveBoard;

            for (int row = coordinate.Row - rowsAboveBoard - 1; row >= 0; --row)
            {
                Coordinate coordinateBelow = new(row, coordinate.Column);

                IPiece piece = board.Get(coordinateBelow);

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