using Game.Common;
using Game.Common.Pieces;
using Game.Gameplay.Board;
using Game.Gameplay.View.Pieces;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity.Pooling;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Player
{
    public class PlayerPieceGhostView : IPlayerPieceGhostView
    {
        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;
        [NotNull] private readonly IPlayerPieceView _playerPieceView;
        [NotNull] private readonly IGameObjectPool _gameObjectPool;

        private InitializedLabel _initializedLabel;

        private Transform _parent;
        private GameObjectPooledInstance? _pooledInstance;

        public GameObject Instance => _pooledInstance?.Instance;

        public PlayerPieceGhostView(
            [NotNull] IBoardContainer boardContainer,
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IPlayerPieceView playerPieceView,
            [NotNull] IGameObjectPool gameObjectPool)
        {
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);
            ArgumentNullException.ThrowIfNull(playerPieceView);
            ArgumentNullException.ThrowIfNull(gameObjectPool);

            _boardContainer = boardContainer;
            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
            _playerPieceView = playerPieceView;
            _gameObjectPool = gameObjectPool;
        }

        public void Initialize()
        {
            _initializedLabel.SetInitialized();

            _parent = new GameObject("PlayerPieceGhostParent").transform; // New game object outside canvas, etc
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            InvalidOperationException.ThrowIfNull(_parent);

            Destroy();

            Object.Destroy(_parent.gameObject);

            _parent = null;
        }

        public void Instantiate(PieceType pieceType)
        {
            if (_pooledInstance.HasValue)
            {
                InvalidOperationException.Throw(); // TODO
            }

            IPieceViewDefinition pieceViewDefinition = _pieceViewDefinitionGetter.Get(pieceType);

            _pooledInstance = _gameObjectPool.Get(pieceViewDefinition.Prefab, _parent);
        }

        public void Destroy()
        {
            if (!_pooledInstance.HasValue)
            {
                InvalidOperationException.Throw(); // TODO
            }

            _pooledInstance.Value.ReturnToPool();
            _pooledInstance = null;
        }
    }
}