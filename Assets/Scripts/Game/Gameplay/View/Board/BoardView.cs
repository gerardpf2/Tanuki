using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public class BoardView : IBoardView
    {
        [NotNull] private readonly IPieceCachedPropertiesGetter _pieceCachedPropertiesGetter;

        private Gameplay.Board.Board _board;
        private Transform _piecesParent;

        public IReadonlyBoard Board
        {
            get
            {
                // TODO: Remove NotNull

                InvalidOperationException.ThrowIfNull(_board);

                return _board;
            }
        }

        public BoardView([NotNull] IPieceCachedPropertiesGetter pieceCachedPropertiesGetter)
        {
            ArgumentNullException.ThrowIfNull(pieceCachedPropertiesGetter);

            _pieceCachedPropertiesGetter = pieceCachedPropertiesGetter;
        }

        public void Initialize([NotNull] IReadonlyBoard board)
        {
            ArgumentNullException.ThrowIfNull(board);

            Uninitialize();

            _board = new Gameplay.Board.Board(_pieceCachedPropertiesGetter, board.Rows, board.Columns);
            _piecesParent = new GameObject("PiecesParent").transform; // New game object outside canvas, etc
        }

        public void Uninitialize()
        {
            // TODO: TryDestroyPiece

            _board = null;

            if (_piecesParent == null)
            {
                return;
            }

            Object.Destroy(_piecesParent);

            _piecesParent = null;
        }

        public GameObject InstantiatePiece([NotNull] IPiece piece, Coordinate sourceCoordinate, [NotNull] GameObject prefab)
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