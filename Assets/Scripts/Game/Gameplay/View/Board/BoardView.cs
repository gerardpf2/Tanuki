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

        [NotNull] private readonly IDictionary<int, IPiece> _pieces = new Dictionary<int, IPiece>();
        [NotNull] private readonly IDictionary<int, GameObject> _pieceInstances = new Dictionary<int, GameObject>();

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

        public IPiece GetPiece(int id)
        {
            if (!_pieces.TryGetValue(id, out IPiece piece))
            {
                InvalidOperationException.Throw($"Piece with Id: {id} cannot be found");
            }

            InvalidOperationException.ThrowIfNull(piece);

            return piece;
        }

        public GameObject GetPieceInstance(int id)
        {
            if (!_pieceInstances.TryGetValue(id, out GameObject pieceInstance))
            {
                InvalidOperationException.Throw($"Piece with Id: {id} cannot be found");
            }

            InvalidOperationException.ThrowIfNull(pieceInstance);

            return pieceInstance;
        }

        public void InstantiatePiece([NotNull] IPiece piece, Coordinate sourceCoordinate, [NotNull] GameObject prefab)
        {
            ArgumentNullException.ThrowIfNull(piece);
            ArgumentNullException.ThrowIfNull(prefab);
            InvalidOperationException.ThrowIfNull(Board);

            int id = piece.Id;

            if (_pieces.ContainsKey(id) || _pieceInstances.ContainsKey(id))
            {
                InvalidOperationException.Throw($"Piece with Id: {id} has already been instantiated");
            }

            Board.Add(piece, sourceCoordinate);

            Vector3 position = GetWorldPosition(sourceCoordinate);
            GameObject pieceInstance = Object.Instantiate(prefab, position, Quaternion.identity, _piecesParent);

            InvalidOperationException.ThrowIfNullWithMessage(
                pieceInstance,
                $"Cannot instantiate piece with Prefab: {prefab.name}"
            );

            _pieces.Add(id, piece);
            _pieceInstances.Add(id, pieceInstance);
        }

        public void DestroyPiece(int id)
        {
            IPiece piece = GetPiece(id);

            Board.Remove(piece);

            GameObject pieceInstance = GetPieceInstance(id);

            Object.Destroy(pieceInstance);

            _pieces.Remove(id);
            _pieceInstances.Remove(id);
        }

        public void MovePiece(int id, int rowOffset, int columnOffset)
        {
            IPiece piece = GetPiece(id);

            Board.Move(piece, rowOffset, columnOffset);

            // Piece instance position should have already been updated externally (using tweens, etc), but it can be
            // set in here too just in case

            GameObject pieceInstance = GetPieceInstance(id);

            Coordinate sourceCoordinate = Board.GetPieceSourceCoordinate(piece);

            pieceInstance.transform.position = GetWorldPosition(sourceCoordinate);
        }

        private void DestroyAllPieces()
        {
            IEnumerable<int> idsCopy = new List<int>(_pieces.Keys);

            foreach (int id in idsCopy)
            {
                DestroyPiece(id);
            }
        }

        private static Vector3 GetWorldPosition(Coordinate sourceCoordinate)
        {
            return new Vector3(sourceCoordinate.Column, sourceCoordinate.Row);
        }
    }
}