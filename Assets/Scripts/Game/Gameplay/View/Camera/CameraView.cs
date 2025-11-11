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

            int newVisibleRows = _camera.VisibleRows;
            float newSize = initialSize / initialVisibleRows * newVisibleRows;

            UnityCamera.orthographicSize = newSize;

            // Correct position y

            float offsetY = bottomPositionY * newSize / initialSize;

            _unityCameraTransform.position = _unityCameraTransform.position.AddY(-offsetY);
        }

        private void SetInitialPosition()
        {
            const int initialRow = 0;
            int initialColumn = ComputeInitialColumn();

            _unityCameraTransform.position = _unityCameraTransform.position.WithX(initialColumn).WithY(initialRow);
        }

        private int ComputeInitialColumn()
        {
            return _board.Columns / 2;
        }
    }
}