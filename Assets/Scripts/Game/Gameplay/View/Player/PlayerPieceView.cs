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
            InvalidOperationException.ThrowIfNotNull(_pieceData);

            IPieceViewDefinition pieceViewDefinition = _pieceViewDefinitionGetter.Get(piece.Type);
            GameObjectPooledInstance pooledInstance = _gameObjectPool.Get(pieceViewDefinition.Prefab, _parent);

            _pieceData = new PieceData(piece, pooledInstance);

            Coordinate = sourceCoordinate;

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

        public bool CanMove(int columnOffset)
        {
            InvalidOperationException.ThrowIfNull(_pieceData);

            IPiece piece = _pieceData.Piece;

            int column = Coordinate.Column + columnOffset;
            int clampedColumn = GetClampedColumn(piece, column);

            return column == clampedColumn;
        }

        public void Move(int columnOffset)
        {
            const int rowOffset = 0;

            if (!CanMove(columnOffset))
            {
                InvalidOperationException.Throw("Cannot be moved");
            }

            Coordinate = Coordinate.WithOffset(rowOffset, columnOffset);

            OnMoved?.Invoke();
        }

        public bool CanRotate()
        {
            InvalidOperationException.ThrowIfNull(_pieceData);

            IPiece piece = _pieceData.Piece;

            return piece.CanRotate;
        }

        public void Rotate()
        {
            if (!CanRotate())
            {
                InvalidOperationException.Throw("Cannot be rotated");
            }

            RotateAndUpdateCoordinate();
            NotifyRotated();

            OnMoved?.Invoke();
            OnRotated?.Invoke();

            return;

            void RotateAndUpdateCoordinate()
            {
                InvalidOperationException.ThrowIfNull(_pieceData);

                IPiece piece = _pieceData.Piece;

                int height = piece.Height;
                int width = piece.Width;

                ++piece.Rotation;

                int heightAfterRotate = piece.Height;
                int widthAfterRotate = piece.Width;

                int rowOffset = height - heightAfterRotate;
                int columnOffset = (width - widthAfterRotate) / 2;

                Coordinate coordinate = Coordinate;

                int row = coordinate.Row + rowOffset;
                int column = coordinate.Column + columnOffset;

                Coordinate = new Coordinate(row, GetClampedColumn(piece, column));
            }

            void NotifyRotated()
            {
                GameObject instance = Instance;

                InvalidOperationException.ThrowIfNull(instance);

                IPieceViewEventNotifier pieceViewEventNotifier = instance.GetComponent<IPieceViewEventNotifier>();

                InvalidOperationException.ThrowIfNull(pieceViewEventNotifier);

                pieceViewEventNotifier.OnRotated();
            }
        }

        private int GetClampedColumn([NotNull] IPiece piece, int column)
        {
            ArgumentNullException.ThrowIfNull(piece);

            const int minColumn = 0;
            int maxColumn = Math.Max(_board.Columns - piece.Width, minColumn);

            return Math.Clamp(column, minColumn, maxColumn);
        }
    }
}