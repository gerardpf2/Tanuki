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
        private const int ExtraRowsOnTop = 5; // TODO: Scriptable object for this and other camera params

        [NotNull] private readonly Transform _cameraTransform;

        public CameraController([NotNull] ICameraGetter cameraGetter)
        {
            ArgumentNullException.ThrowIfNull(cameraGetter);

            _cameraTransform = cameraGetter.GetMain().transform;
        }

        public void Initialize([NotNull] IReadonlyBoard board, float boardViewTopY, float boardViewBottomY)
        {
            // TODO: Check allow multiple Initialize. Add Clear ¿?

            ArgumentNullException.ThrowIfNull(board);

            float x = Mathf.Floor(0.5f * board.Columns);
            float y = Mathf.Max(board.HighestNonEmptyRow + ExtraRowsOnTop - boardViewTopY, -boardViewBottomY);

            _cameraTransform.position = _cameraTransform.position.WithX(x).WithY(y);
        }
    }
}