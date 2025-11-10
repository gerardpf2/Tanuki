using System.Collections.Generic;
using Game.Common;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Pieces;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity.Pooling;
using JetBrains.Annotations;
using UnityEngine;
using ILogger = Infrastructure.Logging.ILogger;

namespace Game.Gameplay.View.Board
{
    public class BoardView : IBoardView
    {
        [NotNull] private readonly IBoard _modelBoard;
        [NotNull] private readonly IBoard _viewBoard;
        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;
        [NotNull] private readonly ILogger _logger;
        [NotNull] private readonly IGameObjectPool _gameObjectPool;

        [NotNull] private readonly IDictionary<int, GameObjectPooledInstance> _piecePooledInstances = new Dictionary<int, GameObjectPooledInstance>();

        private InitializedLabel _initializedLabel;

        private Transform _piecesParent;

        public BoardView(
            [NotNull] IBoard modelBoard,
            [NotNull] IBoard viewBoard,
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] ILogger logger,
            [NotNull] IGameObjectPool gameObjectPool)
        {
            ArgumentNullException.ThrowIfNull(modelBoard);
            ArgumentNullException.ThrowIfNull(viewBoard);
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(gameObjectPool);

            _modelBoard = modelBoard;
            _viewBoard = viewBoard;
            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
            _logger = logger;
            _gameObjectPool = gameObjectPool;
        }

        public void Initialize()
        {
            _initializedLabel.SetInitialized();

            _piecesParent = new GameObject("PiecesParent").transform; // New game object outside canvas, etc

            _viewBoard.Build(_modelBoard.Columns);
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            InvalidOperationException.ThrowIfNull(_piecesParent);

            DestroyAllPieces();

            Object.Destroy(_piecesParent.gameObject);

            _piecesParent = null;

            _viewBoard.Clear();
        }

        public IPiece GetPiece(int pieceId)
        {
            return _viewBoard.GetPiece(pieceId);
        }

        public GameObject GetPieceInstance(int pieceId)
        {
            return GetPiecePooledInstance(pieceId).Instance;
        }

        public void InstantiatePiece([NotNull] IPiece piece, Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(piece);
            InvalidOperationException.ThrowIfNull(_piecesParent);

            int pieceId = piece.Id;

            if (_piecePooledInstances.ContainsKey(pieceId))
            {
                InvalidOperationException.Throw($"Piece with Id: {pieceId} has already been instantiated");
            }

            _viewBoard.AddPiece(piece, sourceCoordinate);

            IPieceViewDefinition pieceViewDefinition = _pieceViewDefinitionGetter.Get(piece.Type);
            Vector3 position = sourceCoordinate.ToVector3();
            GameObjectPooledInstance piecePooledInstance = _gameObjectPool.Get(pieceViewDefinition.Prefab, _piecesParent);

            piecePooledInstance.Instance.transform.position = position;

            _piecePooledInstances.Add(pieceId, piecePooledInstance);
        }

        public void DestroyPiece(int pieceId)
        {
            _viewBoard.RemovePiece(pieceId);

            GameObjectPooledInstance piecePooledInstance = GetPiecePooledInstance(pieceId);

            piecePooledInstance.ReturnToPool();

            _piecePooledInstances.Remove(pieceId);
        }

        public void MovePiece(int pieceId, int rowOffset, int columnOffset)
        {
            _viewBoard.MovePiece(pieceId, rowOffset, columnOffset);

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
            GameObject pieceInstance = GetPieceInstance(pieceId);

            Coordinate sourceCoordinate = _viewBoard.GetSourceCoordinate(pieceId);

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