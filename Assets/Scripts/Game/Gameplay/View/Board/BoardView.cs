using System.Collections.Generic;
using Game.Common;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity.Pooling;
using JetBrains.Annotations;
using UnityEngine;
using ILogger = Infrastructure.Logging.ILogger;

namespace Game.Gameplay.View.Board
{
    public class BoardView : IBoardView
    {
        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly ILogger _logger;
        [NotNull] private readonly IGameObjectPool _gameObjectPool;

        [NotNull] private readonly IDictionary<int, GameObjectPooledInstance> _piecePooledInstances = new Dictionary<int, GameObjectPooledInstance>();

        private InitializedLabel _initializedLabel;

        private IBoard _board;
        private Transform _piecesParent;

        public BoardView(
            [NotNull] IBoardContainer boardContainer,
            [NotNull] ILogger logger,
            [NotNull] IGameObjectPool gameObjectPool)
        {
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(gameObjectPool);

            _boardContainer = boardContainer;
            _logger = logger;
            _gameObjectPool = gameObjectPool;
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

            DestroyAllPieces();

            Object.Destroy(_piecesParent.gameObject);

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
            return GetPiecePooledInstance(pieceId).Instance;
        }

        public void InstantiatePiece([NotNull] IPiece piece, Coordinate sourceCoordinate, [NotNull] GameObject prefab)
        {
            ArgumentNullException.ThrowIfNull(piece);
            ArgumentNullException.ThrowIfNull(prefab);
            InvalidOperationException.ThrowIfNull(_board);
            InvalidOperationException.ThrowIfNull(_piecesParent);

            int pieceId = piece.Id;

            if (_piecePooledInstances.ContainsKey(pieceId))
            {
                InvalidOperationException.Throw($"Piece with Id: {pieceId} has already been instantiated");
            }

            _board.AddPiece(piece, sourceCoordinate);

            Vector3 position = sourceCoordinate.ToVector3();
            GameObjectPooledInstance piecePooledInstance = _gameObjectPool.Get(prefab, _piecesParent);

            piecePooledInstance.Instance.transform.position = position;

            _piecePooledInstances.Add(pieceId, piecePooledInstance);
        }

        public void DestroyPiece(int pieceId)
        {
            InvalidOperationException.ThrowIfNull(_board);

            _board.RemovePiece(pieceId);

            GameObjectPooledInstance piecePooledInstance = GetPiecePooledInstance(pieceId);

            piecePooledInstance.ReturnToPool();

            _piecePooledInstances.Remove(pieceId);
        }

        public void MovePiece(int pieceId, int rowOffset, int columnOffset)
        {
            InvalidOperationException.ThrowIfNull(_board);

            _board.MovePiece(pieceId, rowOffset, columnOffset);

            EnsurePiecePositionIsExpected(pieceId);
        }

        private void DestroyAllPieces()
        {
            IEnumerable<int> pieceIds = new List<int>(_piecePooledInstances.Keys);

            foreach (int pieceId in pieceIds)
            {
                DestroyPiece(pieceId);
            }
        }

        private GameObjectPooledInstance GetPiecePooledInstance(int pieceId)
        {
            if (!_piecePooledInstances.TryGetValue(pieceId, out GameObjectPooledInstance piecePooledInstance))
            {
                InvalidOperationException.Throw($"Piece with Id: {pieceId} cannot be found");
            }

            return piecePooledInstance;
        }

        private void EnsurePiecePositionIsExpected(int pieceId)
        {
            InvalidOperationException.ThrowIfNull(_board);

            GameObject pieceInstance = GetPieceInstance(pieceId);

            Coordinate sourceCoordinate = _board.GetSourceCoordinate(pieceId);

            Vector3 expectedWorldPosition = sourceCoordinate.ToVector3();
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