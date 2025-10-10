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
    public class PiecePlayerView : IPiecePlayerView
    {
        private sealed class PieceData
        {
            public readonly int TopMostRowOffset;
            public readonly int RightMostColumnOffset;
            public readonly GameObject Instance;

            public float X { get; set; }

            public PieceData(
                [NotNull] IPieceCachedPropertiesGetter pieceCachedPropertiesGetter,
                IPiece piece,
                GameObject instance)
            {
                ArgumentNullException.ThrowIfNull(pieceCachedPropertiesGetter);

                TopMostRowOffset = pieceCachedPropertiesGetter.GetTopMostRowOffset(piece);
                RightMostColumnOffset = pieceCachedPropertiesGetter.GetRightMostColumnOffset(piece);
                Instance = instance;
            }
        }

        [NotNull] private readonly IPieceCachedPropertiesGetter _pieceCachedPropertiesGetter;
        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly ICamera _camera;
        [NotNull] private readonly IWorldPositionGetter _worldPositionGetter;

        private InitializedLabel _initializedLabel;

        private Transform _playerPieceParent;
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

        public PiecePlayerView(
            [NotNull] IPieceCachedPropertiesGetter pieceCachedPropertiesGetter,
            [NotNull] IBoardContainer boardContainer,
            [NotNull] ICamera camera,
            [NotNull] IWorldPositionGetter worldPositionGetter)
        {
            ArgumentNullException.ThrowIfNull(pieceCachedPropertiesGetter);
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(camera);
            ArgumentNullException.ThrowIfNull(worldPositionGetter);

            _pieceCachedPropertiesGetter = pieceCachedPropertiesGetter;
            _boardContainer = boardContainer;
            _camera = camera;
            _worldPositionGetter = worldPositionGetter;
        }

        public void Initialize()
        {
            _initializedLabel.SetInitialized();

            _playerPieceParent = new GameObject("PlayerPieceParent").transform; // New game object outside canvas, etc
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            InvalidOperationException.ThrowIfNull(_playerPieceParent);

            Object.Destroy(_playerPieceParent.gameObject);

            _playerPieceParent = null;
            _pieceData = null;
        }

        public void Instantiate(IPiece piece, [NotNull] GameObject prefab)
        {
            ArgumentNullException.ThrowIfNull(prefab);
            InvalidOperationException.ThrowIfNotNull(_pieceData);

            GameObject instance = Object.Instantiate(prefab, _playerPieceParent);

            InvalidOperationException.ThrowIfNullWithMessage(
                instance,
                $"Cannot instantiate player piece with Prefab: {prefab.name}"
            );

            _pieceData = new PieceData(_pieceCachedPropertiesGetter, piece, instance);

            Transform transform = Instance.transform;

            transform.position = GetInitialPosition();

            _pieceData.X = transform.position.x;
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

            _pieceData.X = ClampX(_pieceData.X + deltaX);

            Transform transform = Instance.transform;

            transform.position = transform.position.WithX(Mathf.RoundToInt(_pieceData.X));
        }

        private Vector3 GetInitialPosition()
        {
            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);
            InvalidOperationException.ThrowIfNull(_pieceData);

            int row = _camera.TopRow - _pieceData.TopMostRowOffset;
            int column = (board.Columns - _pieceData.RightMostColumnOffset) / 2;

            float x = _worldPositionGetter.GetX(column);
            float y = _worldPositionGetter.GetY(row);

            return new Vector3(ClampX(x), y);
        }

        private float ClampX(float x)
        {
            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);
            InvalidOperationException.ThrowIfNull(_pieceData);

            const int minColumn = 0;
            int maxColumn = Mathf.Max(board.Columns - 1 - _pieceData.RightMostColumnOffset, minColumn);

            float minX = _worldPositionGetter.GetX(minColumn);
            float maxX = _worldPositionGetter.GetX(maxColumn);

            return Mathf.Clamp(x, minX, maxX);
        }
    }
}