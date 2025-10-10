using System;
using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Board.Pieces;
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
        public static IEnumerable<int> GetIdsSortedByRowThenByColumn([NotNull] this IBoard board)
        {
            ArgumentNullException.ThrowIfNull(board);

            return
                board.Ids
                    .Select(SelectIdSourceCoordinate)
                    .OrderBy(SelectRow)
                    .ThenBy(SelectColumn)
                    .Select(SelectId);

            (int, Coordinate) SelectIdSourceCoordinate(int id)
            {
                return (id, board.GetSourceCoordinate(id));
            }

            int SelectRow((int _, Coordinate sourceCoordinate) idSourceCoordinate)
            {
                return idSourceCoordinate.sourceCoordinate.Row;
            }

            int SelectColumn((int _, Coordinate sourceCoordinate) idSourceCoordinate)
            {
                return idSourceCoordinate.sourceCoordinate.Column;
            }

            int SelectId((int id, Coordinate _) idSourceCoordinate)
            {
                return idSourceCoordinate.id;
            }
        }

        [NotNull]
        public static IEnumerable<KeyValuePair<int, int>> GetIdsInRow([NotNull] this IBoard board, int row)
        {
            ArgumentNullException.ThrowIfNull(board);

            int columns = board.Columns;

            for (int column = 0; column < columns; ++column)
            {
                Coordinate coordinate = new(row, column);

                int? id = board.Get(coordinate);

                if (!id.HasValue)
                {
                    continue;
                }

                yield return new KeyValuePair<int, int>(id.Value, column);
            }
        }

        public static void GetPieceRowColumnOffset(
            [NotNull] this IBoard board,
            int id,
            int row,
            int column,
            out int rowOffset,
            out int columnOffset)
        {
            ArgumentNullException.ThrowIfNull(board);

            Coordinate sourceCoordinate = board.GetSourceCoordinate(id);

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

        private static int ComputePieceFallImpl([NotNull] this IBoard board, int ignoreId, Coordinate coordinate)
        {
            ArgumentNullException.ThrowIfNull(board);

            int rowsAboveBoard = Math.Max(coordinate.Row - board.Rows, 0);
            int fall = rowsAboveBoard;

            for (int row = coordinate.Row - rowsAboveBoard - 1; row >= 0; --row)
            {
                Coordinate coordinateBelow = new(row, coordinate.Column);

                int? id = board.Get(coordinateBelow);

                if (id.HasValue && id != ignoreId)
                {
                    break;
                }

                ++fall;
            }

            return fall;
        }
    }
}