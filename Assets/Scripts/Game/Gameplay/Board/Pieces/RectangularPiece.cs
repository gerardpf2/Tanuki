using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Pieces
{
    public abstract class RectangularPiece : Piece
    {
        private readonly int _rows;
        private readonly int _columns;

        protected RectangularPiece([NotNull] IConverter converter, int id, PieceType type, int rows, int columns) : base(converter, id, type)
        {
            _rows = rows;
            _columns = columns;
        }

        protected override bool IsInside(int rowOffset, int columnOffset)
        {
            return rowOffset >= 0 && rowOffset < _rows && columnOffset >= 0 && columnOffset < _columns;
        }

        protected override bool[,] GetGrid()
        {
            bool[,] grid = new bool[_rows, _columns];

            for (int row = 0; row < _rows; ++row)
            {
                for (int column = 0; column < _columns; ++column)
                {
                    grid[row, column] = true;
                }
            }

            return grid;
        }
    }
}