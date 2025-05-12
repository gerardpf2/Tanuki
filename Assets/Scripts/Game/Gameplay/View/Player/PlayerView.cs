using System.Diagnostics.CodeAnalysis;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.View.Camera;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity.Utils;
using UnityEngine;

namespace Game.Gameplay.View.Player
{
    public class PlayerView : IPlayerView
    {
        [NotNull] private readonly ICameraBoardViewGetter _cameraBoardViewGetter;

        private Transform _playerPieceParent;
        private Transform _instanceTransform;

        public PlayerView([NotNull] ICameraBoardViewGetter cameraBoardViewGetter)
        {
            ArgumentNullException.ThrowIfNull(cameraBoardViewGetter);

            _cameraBoardViewGetter = cameraBoardViewGetter;
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

            return instance;
        }

        public void Move(float deltaX)
        {
            InvalidOperationException.ThrowIfNull(_instanceTransform);

            _instanceTransform.position = _instanceTransform.position.WithX(_instanceTransform.position.x + deltaX);
        }

        private Vector3 GetWorldPosition()
        {
            return new Vector3(_cameraBoardViewGetter.Column, _cameraBoardViewGetter.VisibleTopRow);
        }
    }
}