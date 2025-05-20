using System.Collections.Generic;
using Game.Gameplay.Board.Utils;
using Infrastructure.System;
using Infrastructure.System.Exceptions;

namespace Game.Gameplay.Board.Pieces
{
    public abstract class RectangularPiece : IPiece, IPieceUpdater
    {
        [Is(ComparisonOperator.GreaterThan, 0)] private readonly int _rows;
        [Is(ComparisonOperator.GreaterThan, 0)] private readonly int _columns;

        public PieceType Type { get; }

        public bool Alive { get; protected set; } = true;

        protected RectangularPiece(
            PieceType type,
            [Is(ComparisonOperator.GreaterThan, 0)] int rows,
            [Is(ComparisonOperator.GreaterThan, 0)] int columns)
        {
            ArgumentOutOfRangeException.ThrowIfNot(rows, ComparisonOperator.GreaterThan, 0);
            ArgumentOutOfRangeException.ThrowIfNot(columns, ComparisonOperator.GreaterThan, 0);

            Type = type;

            _rows = rows;
            _columns = columns;
        }

        public IEnumerable<Coordinate> GetCoordinates(Coordinate sourceCoordinate)
        {
            return sourceCoordinate.Rect(_rows, _columns);
        }

        public void Damage(int rowOffset, int columnOffset)
        {
            ArgumentOutOfRangeException.ThrowIfNot(rowOffset, ComparisonOperator.GreaterThanOrEqualTo, 0);
            ArgumentOutOfRangeException.ThrowIfNot(rowOffset, ComparisonOperator.LessThan, _rows);
            ArgumentOutOfRangeException.ThrowIfNot(columnOffset, ComparisonOperator.GreaterThanOrEqualTo, 0);
            ArgumentOutOfRangeException.ThrowIfNot(columnOffset, ComparisonOperator.LessThan, _columns);

            HandleDamaged(rowOffset, columnOffset);
        }

        protected virtual void HandleDamaged(int rowOffset, int columnOffset)
        {
            Alive = false;
        }
    }
}