using Infrastructure.System;
using Infrastructure.System.Matrix.Utils;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
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

        protected override bool[,] GetGrid()
        {
            bool[,] grid = new bool[_rows, _columns];

            grid.Fill(true);

            return grid;
        }
    }
}