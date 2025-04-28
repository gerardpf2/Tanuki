using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Infrastructure.System;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public class BoardView : IBoardView
    {
        private Gameplay.Board.Board _board;

        public void Initialize(
            [Is(ComparisonOperator.GreaterThanOrEqualTo, 0)] int rows,
            [Is(ComparisonOperator.GreaterThanOrEqualTo, 0)] int columns)
        {
            ArgumentOutOfRangeException.ThrowIfNot(rows, ComparisonOperator.GreaterThanOrEqualTo, 0);
            ArgumentOutOfRangeException.ThrowIfNot(columns, ComparisonOperator.GreaterThanOrEqualTo, 0);

            _board = new Gameplay.Board.Board(rows, columns);

            // TODO
        }

        public void Add([NotNull] IPiece piece, Coordinate sourceCoordinate, GameObject instance)
        {
            ArgumentNullException.ThrowIfNull(piece);
            InvalidOperationException.ThrowIfNull(_board);

            _board.Add(piece, sourceCoordinate);

            // TODO
        }
    }
}