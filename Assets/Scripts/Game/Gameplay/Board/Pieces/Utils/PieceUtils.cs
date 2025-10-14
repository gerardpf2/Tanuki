using System.Collections.Generic;
using Game.Gameplay.Board.Utils;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Pieces.Utils
{
    public static class PieceUtils
    {
        public static int GetWidth([NotNull] this IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            bool[,] grid = piece.Grid;

            int columns = grid.GetLength(1);

            return columns; // Assumes piece grid has no empty column
        }

        public static int GetHeight([NotNull] this IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            bool[,] grid = piece.Grid;

            int rows = grid.GetLength(0);

            return rows; // Assumes piece grid has no empty row
        }

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

        [NotNull]
        public static IPiece WithState([NotNull] this IPiece piece, IEnumerable<KeyValuePair<string, string>> state)
        {
            ArgumentNullException.ThrowIfNull(piece);

            piece.ProcessState(state);

            return piece;
        }
    }
}