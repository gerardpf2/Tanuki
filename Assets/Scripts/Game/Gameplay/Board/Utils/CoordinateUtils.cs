using System.Collections.Generic;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Utils
{
    public static class CoordinateUtils
    {
        public static Coordinate WithOffset(this Coordinate coordinate, int rowOffset, int columnOffset)
        {
            return new Coordinate(coordinate.Row + rowOffset, coordinate.Column + columnOffset);
        }

        [NotNull]
        public static IEnumerable<Coordinate> Rect(this Coordinate coordinate, int rows, int columns)
        {
            for (int rowOffset = 0; rowOffset < rows; ++rowOffset)
            {
                for (int columnOffset = 0; columnOffset < columns; ++columnOffset)
                {
                    yield return coordinate.WithOffset(rowOffset, columnOffset);
                }
            }
        }
    }
}