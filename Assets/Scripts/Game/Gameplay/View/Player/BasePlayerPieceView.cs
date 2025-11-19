using System;
using Game.Common;
using Game.Common.Pieces;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Pieces;
using Infrastructure.Unity.Pooling;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;
using Object = UnityEngine.Object;

namespace Game.Gameplay.View.Player
{
    public abstract class BasePlayerPieceView : IBasePlayerPieceView
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

        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;
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
                GameObject instance = Instance;

                InvalidOperationException.ThrowIfNull(instance);

                return instance.transform.position.ToCoordinate();
            }
            private set
            {
                GameObject instance = Instance;

                InvalidOperationException.ThrowIfNull(instance);

                instance.transform.position = value.ToVector3();
            }
        }

        public GameObject Instance => _pieceData?.PooledInstance.Instance;

        protected abstract string ParentName { get; }

        protected BasePlayerPieceView(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IGameObjectPool gameObjectPool)
        {
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);
            ArgumentNullException.ThrowIfNull(gameObjectPool);

            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
            _gameObjectPool = gameObjectPool;
        }

        public virtual void Initialize()
        {
            _initializedLabel.SetInitialized();

            _parent = new GameObject(ParentName).transform; // New game object outside canvas, etc
        }

        public virtual void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            InvalidOperationException.ThrowIfNull(_parent);

            Destroy();

            Object.Destroy(_parent.gameObject);

            _parent = null;
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

        protected void InstantiateImpl([NotNull] IPiece piece, Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(piece);
            InvalidOperationException.ThrowIfNotNull(_pieceData);

            IPieceViewDefinition pieceViewDefinition = GetPieceViewDefinition(_pieceViewDefinitionGetter, piece.Type);
            GameObjectPooledInstance pooledInstance = _gameObjectPool.Get(pieceViewDefinition.Prefab, _parent);

            _pieceData = new PieceData(piece, pooledInstance);

            Coordinate = sourceCoordinate;

            OnInstantiated?.Invoke();
        }

        [NotNull]
        protected abstract IPieceViewDefinition GetPieceViewDefinition(
            IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            PieceType pieceType
        );
    }
}