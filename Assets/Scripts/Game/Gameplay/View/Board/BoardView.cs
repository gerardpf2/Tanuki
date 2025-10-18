using System.Collections.Generic;
using Game.Common;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.Common;
using Game.Gameplay.Common.Utils;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;
using ILogger = Infrastructure.Logging.ILogger;

namespace Game.Gameplay.View.Board
{
    public class BoardView : IBoardView
    {
        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly IWorldPositionGetter _worldPositionGetter;
        [NotNull] private readonly ILogger _logger;

        [NotNull] private readonly IDictionary<int, GameObject> _pieceInstances = new Dictionary<int, GameObject>();

        private InitializedLabel _initializedLabel;

        private IBoard _board;
        private Transform _piecesParent;

        public BoardView(
            [NotNull] IBoardContainer boardContainer,
            [NotNull] IWorldPositionGetter worldPositionGetter,
            [NotNull] ILogger logger)
        {
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(worldPositionGetter);
            ArgumentNullException.ThrowIfNull(logger);

            _boardContainer = boardContainer;
            _worldPositionGetter = worldPositionGetter;
            _logger = logger;
        }

        public void Initialize()
        {
            _initializedLabel.SetInitialized();

            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            _board = new Gameplay.Board.Board(board.Rows, board.Columns);
            _piecesParent = new GameObject("PiecesParent").transform; // New game object outside canvas, etc
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            InvalidOperationException.ThrowIfNull(_piecesParent);

            Object.Destroy(_piecesParent.gameObject);

            _pieceInstances.Clear();

            _board = null;
            _piecesParent = null;
        }

        public IPiece GetPiece(int pieceId)
        {
            InvalidOperationException.ThrowIfNull(_board);

            return _board.GetPiece(pieceId);
        }

        public GameObject GetPieceInstance(int pieceId)
        {
            if (!_pieceInstances.TryGetValue(pieceId, out GameObject pieceInstance))
            {
                InvalidOperationException.Throw($"Piece with Id: {pieceId} cannot be found");
            }

            InvalidOperationException.ThrowIfNull(pieceInstance);

            return pieceInstance;
        }

        public void InstantiatePiece([NotNull] IPiece piece, Coordinate sourceCoordinate, [NotNull] GameObject prefab)
        {
            ArgumentNullException.ThrowIfNull(piece);
            ArgumentNullException.ThrowIfNull(prefab);
            InvalidOperationException.ThrowIfNull(_board);
            InvalidOperationException.ThrowIfNull(_piecesParent);

            int pieceId = piece.Id;

            if (_pieceInstances.ContainsKey(pieceId))
            {
                InvalidOperationException.Throw($"Piece with Id: {pieceId} has already been instantiated");
            }

            _board.AddPiece(piece, sourceCoordinate);

            Vector3 position = GetWorldPosition(sourceCoordinate);
            GameObject pieceInstance = Object.Instantiate(prefab, position, Quaternion.identity, _piecesParent);

            InvalidOperationException.ThrowIfNullWithMessage(
                pieceInstance,
                $"Cannot instantiate piece with Id: {pieceId} and Prefab: {prefab.name}"
            );

            _pieceInstances.Add(pieceId, pieceInstance);
        }

        public void DestroyPiece(int pieceId)
        {
            InvalidOperationException.ThrowIfNull(_board);

            _board.RemovePiece(pieceId);

            GameObject pieceInstance = GetPieceInstance(pieceId);

            Object.Destroy(pieceInstance);

            _pieceInstances.Remove(pieceId);
        }

        public void MovePiece(int pieceId, int rowOffset, int columnOffset)
        {
            InvalidOperationException.ThrowIfNull(_board);

            _board.MovePiece(pieceId, rowOffset, columnOffset);

            EnsurePiecePositionIsExpected(pieceId);
        }

        private Vector3 GetWorldPosition(Coordinate coordinate)
        {
            return _worldPositionGetter.Get(coordinate);
        }

        private void EnsurePiecePositionIsExpected(int pieceId)
        {
            InvalidOperationException.ThrowIfNull(_board);

            GameObject pieceInstance = GetPieceInstance(pieceId);

            Coordinate sourceCoordinate = _board.GetSourceCoordinate(pieceId);

            Vector3 expectedWorldPosition = GetWorldPosition(sourceCoordinate);
            Vector3 worldPosition = pieceInstance.transform.position;

            if (expectedWorldPosition == worldPosition) // Vector3 == means approximate equality
            {
                return;
            }

            // TODO: Exception
            _logger.Warning($"Piece with Id: {pieceId} position is not the expected one. Its coordinates are {sourceCoordinate} and its world position should be {expectedWorldPosition}, but instead it is {worldPosition}");

            // TODO: Remove
            pieceInstance.transform.position = expectedWorldPosition;
        }
    }
}