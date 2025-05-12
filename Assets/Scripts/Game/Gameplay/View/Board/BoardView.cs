using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public class BoardView : IBoardView
    {
        private Gameplay.Board.Board _board;
        private Transform _piecesParent;

        public IReadonlyBoard Board
        {
            get
            {
                InvalidOperationException.ThrowIfNull(_board);

                return _board;
            }
        }

        public void Initialize([NotNull] IReadonlyBoard board)
        {
            // TODO: Check allow multiple Initialize. Add Clear ¿?

            ArgumentNullException.ThrowIfNull(board);

            _board = new Gameplay.Board.Board(board.Rows, board.Columns);
            _piecesParent = new GameObject("PiecesParent").transform; // New game object outside canvas, etc
        }

        public GameObject Instantiate([NotNull] IPiece piece, Coordinate sourceCoordinate, [NotNull] GameObject prefab)
        {
            ArgumentNullException.ThrowIfNull(piece);
            ArgumentNullException.ThrowIfNull(prefab);
            InvalidOperationException.ThrowIfNull(_board);
            InvalidOperationException.ThrowIfNull(_piecesParent);

            _board.Add(piece, sourceCoordinate);

            Vector3 position = GetWorldPosition(sourceCoordinate);
            GameObject instance = Object.Instantiate(prefab, position, Quaternion.identity, _piecesParent);

            InvalidOperationException.ThrowIfNullWithMessage(
                instance,
                $"Cannot instantiate piece with Prefab: {prefab.name}"
            );

            return instance;
        }

        private static Vector3 GetWorldPosition(Coordinate sourceCoordinate)
        {
            return new Vector3(sourceCoordinate.Column, sourceCoordinate.Row);
        }
    }
}