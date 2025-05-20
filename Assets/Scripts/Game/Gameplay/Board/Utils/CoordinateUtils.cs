using System.Collections.Generic;
using Infrastructure.System;
using Infrastructure.System.Exceptions;
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
        public static IEnumerable<Coordinate> Rect(
            this Coordinate coordinate,
            [Is(ComparisonOperator.GreaterThan, 0)] int rows,
            [Is(ComparisonOperator.GreaterThan, 0)] int columns)
        {
            ArgumentOutOfRangeException.ThrowIfNot(rows, ComparisonOperator.GreaterThan, 0);
            ArgumentOutOfRangeException.ThrowIfNot(columns, ComparisonOperator.GreaterThan, 0);

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