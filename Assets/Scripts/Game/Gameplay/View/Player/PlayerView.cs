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

        public Coordinate Coordinate
        {
            get
            {
                InvalidOperationException.ThrowIfNull(Instance);

                Transform instanceTransform = Instance.transform;

                int row = Mathf.FloorToInt(instanceTransform.position.y);
                int column = Mathf.FloorToInt(instanceTransform.position.x);

                return new Coordinate(row, column);
            }
        }

        public GameObject Instance { get; private set; }

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

        public void Instantiate([NotNull] IPiece piece, [NotNull] GameObject prefab)
        {
            // TODO: Store piece and add GetCoordinates support (GetWorldPosition, Move, etc)

            ArgumentNullException.ThrowIfNull(piece);
            ArgumentNullException.ThrowIfNull(prefab);
            InvalidOperationException.ThrowIfNull(_playerPieceParent);
            InvalidOperationException.ThrowIfNotNull(Instance);

            Vector3 position = GetWorldPosition();

            Instance = Object.Instantiate(prefab, position, Quaternion.identity, _playerPieceParent);

            InvalidOperationException.ThrowIfNullWithMessage(
                Instance,
                $"Cannot instantiate player piece with Prefab: {prefab.name}"
            );

            _instanceX = position.x;
        }

        public void Destroy()
        {
            InvalidOperationException.ThrowIfNull(Instance);

            Object.Destroy(Instance);

            Instance = null;
        }

        public void Move(float deltaX)
        {
            InvalidOperationException.ThrowIfNull(Instance);

            Transform instanceTransform = Instance.transform;

            _instanceX = ClampX(_instanceX + deltaX);
            instanceTransform.position = instanceTransform.position.WithX(Mathf.RoundToInt(_instanceX));
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