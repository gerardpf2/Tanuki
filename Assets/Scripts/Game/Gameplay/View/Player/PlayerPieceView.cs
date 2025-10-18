using System.Diagnostics.CodeAnalysis;
using Game.Common;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.Camera;
using Game.Gameplay.Common;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity.Utils;
using UnityEngine;

namespace Game.Gameplay.View.Player
{
    public class PlayerPieceView : IPlayerPieceView
    {
        private sealed class PieceData
        {
            [NotNull] public readonly IPiece Piece;
            [NotNull] public readonly GameObject Instance;

            public float X { get; set; }

            public PieceData([NotNull] IPiece piece, [NotNull] GameObject instance)
            {
                ArgumentNullException.ThrowIfNull(piece);
                ArgumentNullException.ThrowIfNull(instance);

                Piece = piece;
                Instance = instance;

                X = Instance.transform.position.x;
            }
        }

        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly ICamera _camera;
        [NotNull] private readonly IWorldPositionGetter _worldPositionGetter;

        private InitializedLabel _initializedLabel;

        private Transform _parent;
        private PieceData _pieceData;

        public Coordinate Coordinate
        {
            get
            {
                InvalidOperationException.ThrowIfNull(Instance);

                Transform transform = Instance.transform;

                float originX = _worldPositionGetter.GetX(0);
                float originY = _worldPositionGetter.GetY(0);

                int row = Mathf.FloorToInt(transform.position.y - originY);
                int column = Mathf.FloorToInt(transform.position.x - originX);

                return new Coordinate(row, column);
            }
        }

        public GameObject Instance => _pieceData?.Instance;

        public PlayerPieceView(
            [NotNull] IBoardContainer boardContainer,
            [NotNull] ICamera camera,
            [NotNull] IWorldPositionGetter worldPositionGetter)
        {
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(camera);
            ArgumentNullException.ThrowIfNull(worldPositionGetter);

            _boardContainer = boardContainer;
            _camera = camera;
            _worldPositionGetter = worldPositionGetter;
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

            Object.Destroy(_parent.gameObject);

            _parent = null;
            _pieceData = null;
        }

        public void Instantiate([NotNull] IPiece piece, [NotNull] GameObject prefab)
        {
            ArgumentNullException.ThrowIfNull(piece);
            ArgumentNullException.ThrowIfNull(prefab);
            InvalidOperationException.ThrowIfNull(_parent);
            InvalidOperationException.ThrowIfNotNull(_pieceData);

            GameObject instance = Object.Instantiate(prefab, GetInitialPosition(piece), Quaternion.identity, _parent);

            InvalidOperationException.ThrowIfNullWithMessage(
                instance,
                $"Cannot instantiate player piece with Prefab: {prefab.name}"
            );

            _pieceData = new PieceData(piece, instance);
        }

        public void Destroy()
        {
            InvalidOperationException.ThrowIfNull(_pieceData);
            InvalidOperationException.ThrowIfNull(Instance);

            Object.Destroy(Instance);

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

        private Vector3 GetInitialPosition([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            int row = _camera.TopRow - piece.Height + 1;
            int column = (board.Columns - piece.Width + 1) / 2;

            float x = _worldPositionGetter.GetX(column);
            float y = _worldPositionGetter.GetY(row);

            return new Vector3(ClampX(piece, x), y);
        }

        private float ClampX([NotNull] IPiece piece, float x)
        {
            ArgumentNullException.ThrowIfNull(piece);

            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            const int minColumn = 0;
            int maxColumn = Mathf.Max(board.Columns - piece.Width, minColumn);

            float minX = _worldPositionGetter.GetX(minColumn);
            float maxX = _worldPositionGetter.GetX(maxColumn);

            return Mathf.Clamp(x, minX, maxX);
        }
    }
}