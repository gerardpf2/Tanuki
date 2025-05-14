using System.Diagnostics.CodeAnalysis;
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
        private Transform _instanceTransform;
        private float _instanceX;

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

        public GameObject Instantiate([NotNull] IPiece piece, [NotNull] GameObject prefab)
        {
            ArgumentNullException.ThrowIfNull(piece);
            ArgumentNullException.ThrowIfNull(prefab);
            InvalidOperationException.ThrowIfNull(_playerPieceParent);
            InvalidOperationException.ThrowIfNotNull(_instanceTransform);

            Vector3 position = GetWorldPosition();
            GameObject instance = Object.Instantiate(prefab, position, Quaternion.identity, _playerPieceParent);

            InvalidOperationException.ThrowIfNullWithMessage(
                instance,
                $"Cannot instantiate player piece with Prefab: {prefab.name}"
            );

            _instanceTransform = instance.transform;
            _instanceX = position.x;

            return instance;
        }

        public void Move(float deltaX)
        {
            InvalidOperationException.ThrowIfNull(_instanceTransform);

            _instanceX = ClampX(_instanceX + deltaX);
            _instanceTransform.position = _instanceTransform.position.WithX(Mathf.RoundToInt(_instanceX));
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