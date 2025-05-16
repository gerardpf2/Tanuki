using System;
using Game.Gameplay.Board.Pieces;
using Infrastructure.System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

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

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0)]
        public static int ComputeFall(
            [NotNull] this IReadonlyBoard board,
            [NotNull] IPiece piece,
            Coordinate sourceCoordinate)
        {
            int fall = int.MaxValue;

            foreach (Coordinate coordinate in piece.GetCoordinates(sourceCoordinate))
            {
                fall = Math.Min(board.ComputeFallImpl(piece, coordinate), fall);
            }

            return fall;
        }

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0)]
        private static int ComputeFallImpl(
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