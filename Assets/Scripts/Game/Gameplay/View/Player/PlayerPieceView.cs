using Game.Common;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.Camera;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity.Pooling;
using Infrastructure.Unity.Utils;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Player
{
    public class PlayerPieceView : IPlayerPieceView
    {
        private sealed class PieceData
        {
            [NotNull] public readonly IPiece Piece;
            public GameObjectPooledInstance PooledInstance;

            public float X { get; set; }

            public PieceData([NotNull] IPiece piece, GameObjectPooledInstance pooledInstance)
            {
                ArgumentNullException.ThrowIfNull(piece);

                Piece = piece;
                PooledInstance = pooledInstance;

                X = PooledInstance.Instance.transform.position.x;
            }
        }

        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly ICamera _camera;
        [NotNull] private readonly IGameObjectPool _gameObjectPool;

        private InitializedLabel _initializedLabel;

        private Transform _parent;
        private PieceData _pieceData;

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
            [NotNull] IBoardContainer boardContainer,
            [NotNull] ICamera camera,
            [NotNull] IGameObjectPool gameObjectPool)
        {
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(camera);
            ArgumentNullException.ThrowIfNull(gameObjectPool);

            _boardContainer = boardContainer;
            _camera = camera;
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

        public void Instantiate([NotNull] IPiece piece, [NotNull] GameObject prefab)
        {
            ArgumentNullException.ThrowIfNull(piece);
            ArgumentNullException.ThrowIfNull(prefab);
            InvalidOperationException.ThrowIfNull(_parent);
            InvalidOperationException.ThrowIfNotNull(_pieceData);

            Vector3 position = new(GetInitialColumn(piece), GetInitialRow(piece));
            GameObjectPooledInstance pooledInstance = _gameObjectPool.Get(prefab, _parent);

            pooledInstance.Instance.transform.position = position;

            _pieceData = new PieceData(piece, pooledInstance);
        }

        public void Destroy()
        {
            InvalidOperationException.ThrowIfNull(_pieceData);

            _pieceData.PooledInstance.ReturnToPool();
            _pieceData = null;
        }

        public void Move(float deltaX)
        {
            InvalidOperationException.ThrowIfNull(_pieceData);
            InvalidOperationException.ThrowIfNull(Instance);

            _pieceData.X = ClampX(_pieceData.Piece, _pieceData.X + deltaX);

            Transform transform = Instance.transform;

            transform.position = transform.position.WithX(Mathf.RoundToInt(_pieceData.X));
        }

        public void Rotate()
        {
            InvalidOperationException.ThrowIfNull(_pieceData);
            InvalidOperationException.ThrowIfNull(Instance);

            IPiece piece = _pieceData.Piece;

            int width = piece.Width;
            int withAfterRotate = piece.Height;
            int offsetX = (width - withAfterRotate) / 2;

            ++piece.Rotation;

            Transform transform = Instance.transform;

            float x = ClampX(piece, transform.position.x + offsetX);
            int y = GetInitialRow(piece);

            transform.position = transform.position.WithX(x).WithY(y);

            _pieceData.X = x;

            IPieceViewEventNotifier pieceViewEventNotifier = Instance.GetComponent<IPieceViewEventNotifier>();

            InvalidOperationException.ThrowIfNull(pieceViewEventNotifier);

            pieceViewEventNotifier.OnRotated();
        }

        private int GetInitialRow([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            return _camera.TopRow - piece.Height + 1;
        }

        private int GetInitialColumn([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            return (board.Columns - piece.Width + 1) / 2;
        }

        private float ClampX([NotNull] IPiece piece, float x)
        {
            ArgumentNullException.ThrowIfNull(piece);

            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            const int minColumn = 0;
            int maxColumn = Mathf.Max(board.Columns - piece.Width, minColumn);

            return Mathf.Clamp(x, minColumn, maxColumn);
        }
    }
}