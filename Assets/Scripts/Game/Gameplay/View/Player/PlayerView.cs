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
        [NotNull] private readonly IReadonlyBoardView _boardView;
        [NotNull] private readonly ICameraBoardViewPropertiesGetter _cameraBoardViewPropertiesGetter;

        private Transform _playerPieceParent;
        private float _instanceX;

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

        public GameObject PieceInstance { get; private set; }

        public PlayerView(
            [NotNull] IReadonlyBoardView boardView,
            [NotNull] ICameraBoardViewPropertiesGetter cameraBoardViewPropertiesGetter)
        {
            ArgumentNullException.ThrowIfNull(boardView);
            ArgumentNullException.ThrowIfNull(cameraBoardViewPropertiesGetter);

            _boardView = boardView;
            _cameraBoardViewPropertiesGetter = cameraBoardViewPropertiesGetter;
        }

        public void Initialize()
        {
            // TODO: Check allow multiple Initialize. Add Clear ¿?

            _playerPieceParent = new GameObject("PlayerPieceParent").transform; // New game object outside canvas, etc
        }

        public void InstantiatePiece([NotNull] IPiece piece, [NotNull] GameObject prefab)
        {
            // TODO: Store piece and add GetCoordinates support (GetWorldPosition, Move, etc)

            ArgumentNullException.ThrowIfNull(piece);
            ArgumentNullException.ThrowIfNull(prefab);
            InvalidOperationException.ThrowIfNull(_playerPieceParent);
            InvalidOperationException.ThrowIfNotNull(PieceInstance);

            Vector3 position = GetWorldPosition();

            PieceInstance = Object.Instantiate(prefab, position, Quaternion.identity, _playerPieceParent);

            InvalidOperationException.ThrowIfNullWithMessage(
                PieceInstance,
                $"Cannot instantiate player piece with Prefab: {prefab.name}"
            );

            _instanceX = position.x;
        }

        public void DestroyPiece()
        {
            InvalidOperationException.ThrowIfNull(PieceInstance);

            Object.Destroy(PieceInstance);

            PieceInstance = null;
        }

        public void MovePiece(float deltaX)
        {
            InvalidOperationException.ThrowIfNull(PieceInstance);

            Transform pieceInstanceTransform = PieceInstance.transform;

            _instanceX = ClampX(_instanceX + deltaX);
            pieceInstanceTransform.position = pieceInstanceTransform.position.WithX(Mathf.RoundToInt(_instanceX));
        }

        private Vector3 GetWorldPosition()
        {
            return new Vector3(Mathf.Floor(0.5f * _boardView.Board.Columns), _cameraBoardViewPropertiesGetter.VisibleTopRow);
        }

        private float ClampX(float x)
        {
            return Mathf.Clamp(x, 0.0f, _boardView.Board.Columns - 1);
        }
    }
}