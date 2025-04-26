using System.Collections.Generic;
using Infrastructure.System;
using Infrastructure.System.Exceptions;

namespace Game.Gameplay.Model.Board.Pieces
{
    public abstract class Piece : IPiece
    {
        [Is(ComparisonOperator.GreaterThanOrEqualTo, 1)] private readonly int _rowSize;
        [Is(ComparisonOperator.GreaterThanOrEqualTo, 1)] private readonly int _columnSize;

        protected Piece(
            [Is(ComparisonOperator.GreaterThanOrEqualTo, 1)] int rowSize,
            [Is(ComparisonOperator.GreaterThanOrEqualTo, 1)] int columnSize)
        {
            ArgumentOutOfRangeException.ThrowIfNot(rowSize, ComparisonOperator.GreaterThanOrEqualTo, 1);
            ArgumentOutOfRangeException.ThrowIfNot(columnSize, ComparisonOperator.GreaterThanOrEqualTo, 1);

            _rowSize = rowSize;
            _columnSize = columnSize;
        }

        public IEnumerable<Coordinate> GetCoordinates(Coordinate sourceCoordinate)
        {
            for (int rowOffset = 0; rowOffset < _rowSize; ++rowOffset)
            {
                int row = sourceCoordinate.Row + rowOffset;

                for (int columnOffset = 0; columnOffset < _columnSize; ++columnOffset)
                {
                    int column = sourceCoordinate.Column + columnOffset;

                    yield return new Coordinate(row, column);
                }
            }
        }
    }
}