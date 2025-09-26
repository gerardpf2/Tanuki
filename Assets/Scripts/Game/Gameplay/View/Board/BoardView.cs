using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.Board.Utils;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public class BoardView : IBoardView
    {
        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly IPieceCachedPropertiesGetter _pieceCachedPropertiesGetter;

        [NotNull] private readonly IDictionary<IPiece, GameObject> _pieceInstances = new Dictionary<IPiece, GameObject>();

        private Transform _piecesParent;

        public IBoard Board { get; private set; }

        public BoardView(
            [NotNull] IBoardContainer boardContainer,
            [NotNull] IPieceCachedPropertiesGetter pieceCachedPropertiesGetter)
        {
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(pieceCachedPropertiesGetter);

            _boardContainer = boardContainer;
            _pieceCachedPropertiesGetter = pieceCachedPropertiesGetter;
        }

        public void Initialize()
        {
            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            Uninitialize();

            Board = new Gameplay.Board.Board(_pieceCachedPropertiesGetter, board.Rows, board.Columns);
            _piecesParent = new GameObject("PiecesParent").transform; // New game object outside canvas, etc
        }

        public void Uninitialize()
        {
            DestroyAllPieces();

            Board = null;

            if (_piecesParent == null)
            {
                return;
            }

            Object.Destroy(_piecesParent.gameObject);

            _piecesParent = null;
        }

        public GameObject GetPieceInstance([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            if (!_pieceInstances.TryGetValue(piece, out GameObject pieceInstance))
            {
                InvalidOperationException.Throw("Piece cannot be found");
            }

            InvalidOperationException.ThrowIfNull(pieceInstance);

            return pieceInstance;
        }

        public void InstantiatePiece([NotNull] IPiece piece, Coordinate sourceCoordinate, [NotNull] GameObject prefab)
        {
            ArgumentNullException.ThrowIfNull(piece);
            ArgumentNullException.ThrowIfNull(prefab);
            InvalidOperationException.ThrowIfNull(Board);

            if (_pieceInstances.ContainsKey(piece))
            {
                InvalidOperationException.Throw("Piece has already been instantiated");
            }

            Board.Add(piece, sourceCoordinate);

            Vector3 position = GetWorldPosition(sourceCoordinate);
            GameObject pieceInstance = Object.Instantiate(prefab, position, Quaternion.identity, _piecesParent);

            InvalidOperationException.ThrowIfNullWithMessage(
                pieceInstance,
                $"Cannot instantiate piece with Prefab: {prefab.name}"
            );

            _pieceInstances.Add(piece, pieceInstance);
        }

        public void DestroyPiece([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            Board.Remove(piece);

            GameObject pieceInstance = GetPieceInstance(piece);

            Object.Destroy(pieceInstance);

            _pieceInstances.Remove(piece);
        }

        public void MovePiece([NotNull] IPiece piece, int rowOffset, int columnOffset)
        {
            ArgumentNullException.ThrowIfNull(piece);

            Board.Move(piece, rowOffset, columnOffset);

            // Piece instance position should have already been updated externally (using tweens, etc), but it can be
            // set in here too just in case

            GameObject pieceInstance = GetPieceInstance(piece);

            Coordinate sourceCoordinate = Board.GetPieceSourceCoordinate(piece);

            pieceInstance.transform.position = GetWorldPosition(sourceCoordinate);
        }

        private void DestroyAllPieces()
        {
            foreach (KeyValuePair<IPiece, GameObject> pieceInstance in _pieceInstances)
            {
                InvalidOperationException.ThrowIfNull(pieceInstance.Value);

                Board.Remove(pieceInstance.Key);

                Object.Destroy(pieceInstance.Value);
            }

            _pieceInstances.Clear();
        }

        private static Vector3 GetWorldPosition(Coordinate sourceCoordinate)
        {
            return new Vector3(sourceCoordinate.Column, sourceCoordinate.Row);
        }
    }
}