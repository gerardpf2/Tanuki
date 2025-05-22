using System.Collections.Generic;
using Game.Gameplay.Board.Utils;
using Infrastructure.System;
using Infrastructure.System.Exceptions;

namespace Game.Gameplay.Board.Pieces
{
    public abstract class RectangularPiece : Piece
    {
        [Is(ComparisonOperator.GreaterThan, 0)] private readonly int _rows;
        [Is(ComparisonOperator.GreaterThan, 0)] private readonly int _columns;

        protected RectangularPiece(
            PieceType type,
            IEnumerable<KeyValuePair<string, object>> customData,
            [Is(ComparisonOperator.GreaterThan, 0)] int rows,
            [Is(ComparisonOperator.GreaterThan, 0)] int columns) : base(type, customData)
        {
            ArgumentOutOfRangeException.ThrowIfNot(rows, ComparisonOperator.GreaterThan, 0);
            ArgumentOutOfRangeException.ThrowIfNot(columns, ComparisonOperator.GreaterThan, 0);

            _rows = rows;
            _columns = columns;
        }

        public override IEnumerable<Coordinate> GetCoordinates(Coordinate sourceCoordinate)
        {
            return sourceCoordinate.Rect(_rows, _columns);
        }

        protected override bool IsInside(int rowOffset, int columnOffset)
        {
            return rowOffset >= 0 && rowOffset < _rows && columnOffset >= 0 && columnOffset < _columns;
        }
    }
}