using System;
using System.Collections.Generic;
using Game.Gameplay.Board.Utils;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.Board.Pieces.Utils
{
    public static class PieceUtils
    {
        [NotNull]
        public static IEnumerable<Coordinate> GetCoordinates([NotNull] this IPiece piece, Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(piece);

            bool[,] grid = piece.Grid;

            int rows = grid.GetLength(0);
            int columns = grid.GetLength(1);

            for (int rowOffset = 0; rowOffset < rows; ++rowOffset)
            {
                for (int columnOffset = 0; columnOffset < columns; ++columnOffset)
                {
                    bool isFilled = grid[rowOffset, columnOffset];

                    if (isFilled)
                    {
                        yield return sourceCoordinate.WithOffset(rowOffset, columnOffset);
                    }
                }
            }
        }

        public static bool IsFilled([NotNull] this IPiece piece, int rowOffset, int columnOffset)
        {
            ArgumentNullException.ThrowIfNull(piece);

            if (rowOffset < 0 || columnOffset < 0)
            {
                return false;
            }

            bool[,] grid = piece.Grid;

            int rows = grid.GetLength(0);
            int columns = grid.GetLength(1);

            if (rowOffset >= rows || columnOffset >= columns)
            {
                return false;
            }

            bool isFilled = grid[rowOffset, columnOffset];

            return isFilled;
        }

        public static void GetTopMostRowAndRightMostColumnOffsets(
            [NotNull] this IPiece piece,
            out int topMostRowOffset,
            out int rightMostColumnOffset)
        {
            // TODO: Review

            ArgumentNullException.ThrowIfNull(piece);

            Coordinate sourceCoordinate = new(0, 0);

            int topMostRow = sourceCoordinate.Row;
            int rightMostColumn = sourceCoordinate.Column;

            foreach (Coordinate coordinate in piece.GetCoordinates(sourceCoordinate))
            {
                topMostRow = Math.Max(coordinate.Row, topMostRow);
                rightMostColumn = Math.Max(coordinate.Column, rightMostColumn);
            }

            topMostRowOffset = topMostRow - sourceCoordinate.Row;
            rightMostColumnOffset = rightMostColumn - sourceCoordinate.Column;
        }

        [NotNull]
        public static IPiece WithState([NotNull] this IPiece piece, IEnumerable<KeyValuePair<string, string>> state)
        {
            ArgumentNullException.ThrowIfNull(piece);

            piece.ProcessState(state);

            return piece;
        }
    }
}