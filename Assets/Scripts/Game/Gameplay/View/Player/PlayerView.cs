using System.Diagnostics.CodeAnalysis;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Camera;
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
        [NotNull] private readonly IReadonlyBoardView _boardView;
        [NotNull] private readonly ICameraBoardViewPropertiesGetter _cameraBoardViewPropertiesGetter;

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
            [NotNull] IReadonlyBoardView boardView,
            [NotNull] ICameraBoardViewPropertiesGetter cameraBoardViewPropertiesGetter)
        {
            ArgumentNullException.ThrowIfNull(pieceCachedPropertiesGetter);
            ArgumentNullException.ThrowIfNull(boardView);
            ArgumentNullException.ThrowIfNull(cameraBoardViewPropertiesGetter);

            _pieceCachedPropertiesGetter = pieceCachedPropertiesGetter;
            _boardView = boardView;
            _cameraBoardViewPropertiesGetter = cameraBoardViewPropertiesGetter;
        }

        public void Initialize()
        {
            Uninitialize();

            _playerPieceParent = new GameObject("PlayerPieceParent").transform; // New game object outside canvas, etc
        }

        public void Uninitialize()
        {
            TryDestroyPiece();

            if (_playerPieceParent == null)
            {
                return;
            }

            Object.Destroy(_playerPieceParent);

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
            InvalidOperationException.ThrowIfNull(_pieceData);

            int x = (_boardView.Board.Columns - _pieceData.RightMostColumnOffset) / 2;
            int y = _cameraBoardViewPropertiesGetter.VisibleTopRow - _pieceData.TopMostRowOffset;

            return new Vector3(ClampX(x), y);
        }

        private float ClampX(float x)
        {
            InvalidOperationException.ThrowIfNull(_pieceData);

            const int minX = 0;
            int maxX = Mathf.Max(_boardView.Board.Columns - 1 - _pieceData.RightMostColumnOffset, minX);

            return Mathf.Clamp(x, minX, maxX);
        }
    }
}