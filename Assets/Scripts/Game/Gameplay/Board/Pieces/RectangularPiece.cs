using System.Collections.Generic;
using Game.Gameplay.Board.Utils;
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