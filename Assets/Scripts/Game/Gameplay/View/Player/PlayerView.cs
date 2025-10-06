using System.Diagnostics.CodeAnalysis;
using Game.Common;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.Camera;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity.Utils;
using UnityEngine;

namespace Game.Gameplay.View.Player
{
    public class PlayerView : IPlayerView
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

        private InitializedLabel _initializedLabel;

        private Transform _playerPieceParent;
        private PieceData _pieceData;

        public Coordinate PieceCoordinate
        {
            get
            {
                InvalidOperationException.ThrowIfNull(PieceInstance);

                Transform pieceInstanceTransform = PieceInstance.transform;

                int row = Mathf.FloorToInt(pieceInstanceTransform.position.y);
                int column = Mathf.FloorToInt(pieceInstanceTransform.position.x);

                return new Coordinate(row, column);
            }
        }

        public GameObject PieceInstance => _pieceData?.Instance;

        public PlayerView(
            [NotNull] IPieceCachedPropertiesGetter pieceCachedPropertiesGetter,
            [NotNull] IBoardContainer boardContainer,
            [NotNull] ICamera camera)
        {
            ArgumentNullException.ThrowIfNull(pieceCachedPropertiesGetter);
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(camera);

            _pieceCachedPropertiesGetter = pieceCachedPropertiesGetter;
            _boardContainer = boardContainer;
            _camera = camera;
        }

        public void Initialize()
        {
            _initializedLabel.SetInitialized();

            _playerPieceParent = new GameObject("PlayerPieceParent").transform; // New game object outside canvas, etc
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            // TODO: Optimize

            TryDestroyPiece();

            if (_playerPieceParent == null)
            {
                return;
            }

            Object.Destroy(_playerPieceParent.gameObject);

            _playerPieceParent = null;
        }

        public void InstantiatePiece(IPiece piece, [NotNull] GameObject prefab)
        {
            ArgumentNullException.ThrowIfNull(prefab);
            InvalidOperationException.ThrowIfNotNull(_pieceData);

            GameObject instance = Object.Instantiate(prefab, _playerPieceParent);

            InvalidOperationException.ThrowIfNullWithMessage(
                instance,
                $"Cannot instantiate player piece with Prefab: {prefab.name}"
            );

            _pieceData = new PieceData(_pieceCachedPropertiesGetter, piece, instance);

            Transform pieceInstanceTransform = PieceInstance.transform;

            pieceInstanceTransform.position = GetInitialPosition();

            _pieceData.X = pieceInstanceTransform.position.x;
        }

        public void DestroyPiece()
        {
            InvalidOperationException.ThrowIfNull(_pieceData);
            InvalidOperationException.ThrowIfNull(PieceInstance);

            Object.Destroy(PieceInstance);

            _pieceData = null;
        }

        public void MovePiece(float deltaX)
        {
            InvalidOperationException.ThrowIfNull(_pieceData);
            InvalidOperationException.ThrowIfNull(PieceInstance);

            _pieceData.X = ClampX(_pieceData.X + deltaX);

            Transform pieceInstanceTransform = PieceInstance.transform;

            pieceInstanceTransform.position = pieceInstanceTransform.position.WithX(Mathf.RoundToInt(_pieceData.X));
        }

        private void TryDestroyPiece()
        {
            if (_pieceData is null || PieceInstance == null)
            {
                return;
            }

            DestroyPiece();
        }

        private Vector3 GetInitialPosition()
        {
            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);
            InvalidOperationException.ThrowIfNull(_pieceData);

            int x = (board.Columns - _pieceData.RightMostColumnOffset) / 2;
            int y = _camera.TopRow - _pieceData.TopMostRowOffset;

            return new Vector3(ClampX(x), y);
        }

        private float ClampX(float x)
        {
            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);
            InvalidOperationException.ThrowIfNull(_pieceData);

            const int minX = 0;
            int maxX = Mathf.Max(board.Columns - 1 - _pieceData.RightMostColumnOffset, minX);

            return Mathf.Clamp(x, minX, maxX);
        }
    }
}