using System;
using Game.Common;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Pieces;
using Game.Gameplay.View.Pieces.Pieces;
using Infrastructure.Unity.Pooling;
using Infrastructure.Unity.Utils;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;
using Object = UnityEngine.Object;

namespace Game.Gameplay.View.Player
{
    public class PlayerPieceView : IPlayerPieceView
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
        [NotNull] private readonly IGameObjectPool _gameObjectPool;

        private InitializedLabel _initializedLabel;

        private Transform _parent;
        private PieceData _pieceData;

        public event Action OnInstantiated;
        public event Action OnDestroyed;
        public event Action OnMoved;
        public event Action OnRotated;

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

        public PlayerPieceView(
            [NotNull] IBoard board,
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IGameObjectPool gameObjectPool)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);
            ArgumentNullException.ThrowIfNull(gameObjectPool);

            _board = board;
            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
            _gameObjectPool = gameObjectPool;
        }

        public void Initialize()
        {
            _initializedLabel.SetInitialized();

            _parent = new GameObject("PlayerPieceParent").transform; // New game object outside canvas, etc
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            InvalidOperationException.ThrowIfNull(_parent);

            Destroy();

            Object.Destroy(_parent.gameObject);

            _parent = null;
        }

        public void Instantiate([NotNull] IPiece piece, Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(piece);
            InvalidOperationException.ThrowIfNull(_parent);
            InvalidOperationException.ThrowIfNotNull(_pieceData);

            IPieceViewDefinition pieceViewDefinition = _pieceViewDefinitionGetter.Get(piece.Type);
            GameObjectPooledInstance pooledInstance = _gameObjectPool.Get(pieceViewDefinition.Prefab, _parent);

            pooledInstance.Instance.transform.position = sourceCoordinate.ToVector3();

            _pieceData = new PieceData(piece, pooledInstance);

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

        public bool CanMove(int offsetX)
        {
            InvalidOperationException.ThrowIfNull(_pieceData);

            IPiece piece = _pieceData.Piece;

            const int minColumn = 0;
            int maxColumn = Mathf.Max(_board.Columns - piece.Width, minColumn);

            int column = Coordinate.Column + offsetX;

            return column >= minColumn && column <= maxColumn;
        }

        public void Move(int offsetX)
        {
            InvalidOperationException.ThrowIfNull(_pieceData);
            InvalidOperationException.ThrowIfNull(Instance);

            IPiece piece = _pieceData.Piece;
            Transform transform = Instance.transform;

            float x = ClampX(piece, transform.position.x + offsetX);

            transform.position = transform.position.WithX(x);

            OnMoved?.Invoke();
        }

        public void Rotate()
        {
            InvalidOperationException.ThrowIfNull(_pieceData);
            InvalidOperationException.ThrowIfNull(Instance);

            IPiece piece = _pieceData.Piece;

            int width = piece.Width;
            int height = piece.Height;
            int offsetX = (width - height) / 2;
            int offsetY = height - width;

            ++piece.Rotation;

            Transform transform = Instance.transform;

            float x = ClampX(piece, transform.position.x + offsetX);

            transform.position = transform.position.WithX(x).AddY(offsetY);

            IPieceViewEventNotifier pieceViewEventNotifier = Instance.GetComponent<IPieceViewEventNotifier>();

            InvalidOperationException.ThrowIfNull(pieceViewEventNotifier);

            pieceViewEventNotifier.OnRotated();

            OnMoved?.Invoke();
            OnRotated?.Invoke();
        }

        private float ClampX([NotNull] IPiece piece, float x)
        {
            ArgumentNullException.ThrowIfNull(piece);

            const int minColumn = 0;
            int maxColumn = Mathf.Max(_board.Columns - piece.Width, minColumn);

            return Mathf.Clamp(x, minColumn, maxColumn);
        }
    }
}