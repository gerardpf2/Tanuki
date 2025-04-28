using System.Collections.Generic;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.Board.Utils;
using Infrastructure.System;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Board
{
    public class Board : IBoard
    {
        [NotNull] private readonly IDictionary<IPiece, Coordinate> _pieceSourceCoordinates = new Dictionary<IPiece, Coordinate>();
        [NotNull] private readonly IPiece[,] _pieces;

        public int Rows { get; }

        public int Columns { get; }

        public Board(
            [Is(ComparisonOperator.GreaterThanOrEqualTo, 0)] int rows,
            [Is(ComparisonOperator.GreaterThanOrEqualTo, 0)] int columns)
        {
            ArgumentOutOfRangeException.ThrowIfNot(rows, ComparisonOperator.GreaterThanOrEqualTo, 0);
            ArgumentOutOfRangeException.ThrowIfNot(columns, ComparisonOperator.GreaterThanOrEqualTo, 0);

            Rows = rows;
            Columns = columns;

            _pieces = new IPiece[rows, columns];
        }

        public void Add([NotNull] IPiece piece, Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(piece);

            if (_pieceSourceCoordinates.ContainsKey(piece))
            {
                InvalidOperationException.Throw(); // TODO
            }

            _pieceSourceCoordinates.Add(piece, sourceCoordinate);

            foreach (Coordinate coordinate in piece.GetCoordinates(sourceCoordinate))
            {
                if (Get(coordinate) is not null)
                {
                    InvalidOperationException.Throw(); // TODO
                }

                Set(piece, coordinate);
            }
        }

        public IPiece Get(Coordinate coordinate)
        {
            if (!this.IsInside(coordinate))
            {
                InvalidOperationException.Throw(); // TODO
            }

            return _pieces[coordinate.Row, coordinate.Column];
        }

        public void Remove([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            if (!_pieceSourceCoordinates.TryGetValue(piece, out Coordinate sourceCoordinate))
            {
                InvalidOperationException.Throw(); // TODO
            }

            _pieceSourceCoordinates.Remove(piece);

            foreach (Coordinate coordinate in piece.GetCoordinates(sourceCoordinate))
            {
                if (Get(coordinate) != piece)
                {
                    InvalidOperationException.Throw(); // TODO
                }

                Set(null, coordinate);
            }
        }

        public void Move([NotNull] IPiece piece, int rowOffset, int columnOffset)
        {
            ArgumentNullException.ThrowIfNull(piece);

            if (!_pieceSourceCoordinates.TryGetValue(piece, out Coordinate sourceCoordinate))
            {
                InvalidOperationException.Throw(); // TODO
            }

            Remove(piece);

            Coordinate newSourceCoordinate = sourceCoordinate.WithOffset(rowOffset, columnOffset);

            Add(piece, newSourceCoordinate);
        }

        private void Set(IPiece piece, Coordinate coordinate)
        {
            if (!this.IsInside(coordinate))
            {
                InvalidOperationException.Throw(); // TODO
            }

            _pieces[coordinate.Row, coordinate.Column] = piece;
        }
    }
}