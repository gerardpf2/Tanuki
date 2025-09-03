using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Infrastructure.System;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public class BoardViewController : IBoardViewController
    {
        private Gameplay.Board.Board _board;
        private Transform _piecesParent;

        public void Initialize(
            [Is(ComparisonOperator.GreaterThanOrEqualTo, 0)] int rows,
            [Is(ComparisonOperator.GreaterThanOrEqualTo, 0)] int columns,
            [NotNull] Transform piecesParent)
        {
            ArgumentOutOfRangeException.ThrowIfNot(rows, ComparisonOperator.GreaterThanOrEqualTo, 0);
            ArgumentOutOfRangeException.ThrowIfNot(columns, ComparisonOperator.GreaterThanOrEqualTo, 0);

            _board = new Gameplay.Board.Board(rows, columns);
            _piecesParent = piecesParent;

            // TODO: Prepare view, camera, etc
        }

        public GameObject Instantiate([NotNull] IPiece piece, Coordinate sourceCoordinate, [NotNull] GameObject prefab)
        {
            ArgumentNullException.ThrowIfNull(piece);
            InvalidOperationException.ThrowIfNull(_board);
            InvalidOperationException.ThrowIfNull(_piecesParent);

            _board.Add(piece, sourceCoordinate);

            GameObject instance = Object.Instantiate(prefab, _piecesParent);

            // TODO: Update instance position, etc

            return instance;
        }
    }
}