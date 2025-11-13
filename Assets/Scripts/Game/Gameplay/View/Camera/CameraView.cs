using Game.Common;
using Game.Gameplay.Board;
using Game.Gameplay.Camera;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using Infrastructure.Unity.Utils;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Camera
{
    public class CameraView : ICameraView
    {
        /*
         *
         * ExtraRowsOnBottom has no impact during phases resolution
         * It allows the view to render more rows than the expected ones
         * For example, it can be used to render half a row to see the board ground
         *
         */
        private const float ExtraRowsOnBottom = 0.5f; // TODO: ScriptableObject

        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly ICamera _camera;
        [NotNull] private readonly Transform _unityCameraTransform;

        private InitializedLabel _initializedLabel;

        public UnityEngine.Camera UnityCamera { get; }

        public CameraView([NotNull] IBoard board, [NotNull] ICamera camera, [NotNull] ICameraGetter cameraGetter)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(camera);
            ArgumentNullException.ThrowIfNull(cameraGetter);

            UnityCamera = cameraGetter.GetMain();

            _board = board;
            _camera = camera;
            _unityCameraTransform = UnityCamera.transform;
        }

        public void Initialize()
        {
            _initializedLabel.SetInitialized();

            // TODO: Cache prev unity camera values, position, etc

            SetInitialPosition();
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            // TODO: Restore prev unity camera values, position, etc
        }

        public void SetBoardViewLimits(float topPositionY, float bottomPositionY)
        {
            // Update size

            float initialSize = UnityCamera.orthographicSize;
            float initialVisibleRows = topPositionY - bottomPositionY;

            float newVisibleRows = _camera.VisibleRows + ExtraRowsOnBottom;
            float newSize = initialSize / initialVisibleRows * newVisibleRows;

            UnityCamera.orthographicSize = newSize;

            // Correct position y

            float offsetY = bottomPositionY * newSize / initialSize + ExtraRowsOnBottom;

            _unityCameraTransform.position = _unityCameraTransform.position.AddY(-offsetY);
        }

        private void SetInitialPosition()
        {
            const int y = 0;
            float x = 0.5f * (_board.Columns - 1);

            _unityCameraTransform.position = _unityCameraTransform.position.WithX(x).WithY(y);
        }
    }
}