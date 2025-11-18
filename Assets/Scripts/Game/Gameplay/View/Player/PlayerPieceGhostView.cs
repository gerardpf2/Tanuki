using System;
using Game.Common;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Pieces;
using Game.Gameplay.View.Pieces.Pieces;
using Infrastructure.Unity.Pooling;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;
using Object = UnityEngine.Object;

namespace Game.Gameplay.View.Player
{
    public class PlayerPieceGhostView : IPlayerPieceGhostView
    {
        private sealed class PieceData
        {
            [NotNull] public readonly IPiece Piece;
            public GameObjectPooledInstance PooledInstance;

            public PieceData([NotNull] IPiece piece, GameObjectPooledInstance pooledInstance)
            {
                ArgumentNullException.ThrowIfNull(piece);

                Piece = piece;
                PooledInstance = pooledInstance;
            }
        }

        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;
        [NotNull] private readonly IPlayerPieceView _playerPieceView;
        [NotNull] private readonly IGameObjectPool _gameObjectPool;

        private InitializedLabel _initializedLabel;

        private Transform _parent;
        private PieceData _pieceData;

        public event Action OnInstantiated;
        public event Action OnDestroyed;

        public Coordinate Coordinate
        {
            get
            {
                InvalidOperationException.ThrowIfNull(Instance);

                Transform transform = Instance.transform;

                return transform.position.ToCoordinate();
            }
        }

        public GameObject Instance => _pieceData?.PooledInstance.Instance;

        public PlayerPieceGhostView(
            [NotNull] IBoard board,
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IPlayerPieceView playerPieceView,
            [NotNull] IGameObjectPool gameObjectPool)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);
            ArgumentNullException.ThrowIfNull(playerPieceView);
            ArgumentNullException.ThrowIfNull(gameObjectPool);

            _board = board;
            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
            _playerPieceView = playerPieceView;
            _gameObjectPool = gameObjectPool;
        }

        public void Initialize()
        {
            _initializedLabel.SetInitialized();

            _parent = new GameObject("PlayerPieceGhostParent").transform; // New game object outside canvas, etc

            SubscribeToEvents();
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            InvalidOperationException.ThrowIfNull(_parent);

            Destroy();

            Object.Destroy(_parent.gameObject);

            _parent = null;

            UnsubscribeFromEvents();
        }

        public void Instantiate([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);
            InvalidOperationException.ThrowIfNotNull(_pieceData);

            IPieceViewDefinition pieceViewDefinition = _pieceViewDefinitionGetter.GetGhost(piece.Type);
            GameObjectPooledInstance pooledInstance = _gameObjectPool.Get(pieceViewDefinition.Prefab, _parent);

            _pieceData = new PieceData(piece, pooledInstance);

            UpdatePosition();

            OnInstantiated?.Invoke();
        }

        public void Destroy()
        {
            if (_pieceData is null)
            {
                return;
            }

            _pieceData.PooledInstance.ReturnToPool();
            _pieceData = null;

            OnDestroyed?.Invoke();
        }

        private void SubscribeToEvents()
        {
            UnsubscribeFromEvents();

            _playerPieceView.OnMoved += HandleMoved;
            _playerPieceView.OnRotated += HandleRotated;
        }

        private void UnsubscribeFromEvents()
        {
            _playerPieceView.OnMoved -= HandleMoved;
            _playerPieceView.OnRotated -= HandleRotated;
        }

        private void UpdatePosition()
        {
            InvalidOperationException.ThrowIfNull(_pieceData);
            InvalidOperationException.ThrowIfNull(Instance);

            Coordinate sourceCoordinate = _playerPieceView.Coordinate;
            Coordinate lockSourceCoordinate = _board.GetPieceLockSourceCoordinate(_pieceData.Piece, sourceCoordinate);

            Instance.transform.position = lockSourceCoordinate.ToVector3();
        }

        private void HandleMoved()
        {
            UpdatePosition();
        }

        private void HandleRotated()
        {
            InvalidOperationException.ThrowIfNull(Instance);

            IPieceViewEventNotifier pieceViewEventNotifier = Instance.GetComponent<IPieceViewEventNotifier>();

            InvalidOperationException.ThrowIfNull(pieceViewEventNotifier);

            pieceViewEventNotifier.OnRotated();
        }
    }
}