using Game.Gameplay.Board;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using Infrastructure.Unity.Utils;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Camera
{
    public class CameraController : ICameraController
    {
        [NotNull] private readonly UnityEngine.Camera _camera; // TODO: Remove and keep transform only if no camera actions need to be done
        [NotNull] private readonly Transform _cameraTransform;

        public CameraController([NotNull] ICameraGetter cameraGetter)
        {
            ArgumentNullException.ThrowIfNull(cameraGetter);

            _camera = cameraGetter.GetMain();
            _cameraTransform = _camera.transform;
        }

        public void Initialize([NotNull] IReadonlyBoard board, float viewBottomY)
        {
            // TODO: Check allow multiple Initialize. Add Clear ¿?

            ArgumentNullException.ThrowIfNull(board);

            _cameraTransform.position = _cameraTransform.position.WithX(0.5f * board.Columns).WithY(-viewBottomY);
        }
    }
}