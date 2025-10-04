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
        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly ICamera _camera;
        [NotNull] private readonly UnityEngine.Camera _unityCamera;
        [NotNull] private readonly Transform _unityCameraTransform;

        private float _bottomPositionYAfterResize;

        public CameraView(
            [NotNull] IBoardContainer boardContainer,
            [NotNull] ICamera camera,
            [NotNull] ICameraGetter cameraGetter)
        {
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(camera);
            ArgumentNullException.ThrowIfNull(cameraGetter);

            _boardContainer = boardContainer;
            _camera = camera;
            _unityCamera = cameraGetter.GetMain();
            _unityCameraTransform = _unityCamera.transform;
        }

        public void Initialize()
        {
            // TODO: Cache prev unity camera values, position, etc

            SetInitialPosition();
        }

        public void Uninitialize()
        {
            // TODO: Restore prev unity camera values, position, etc
        }

        public void SetBoardViewLimits(float topPositionY, float bottomPositionY)
        {
            UpdateSize(topPositionY, bottomPositionY);
            UpdatePosition(_camera.TopRow, _camera.BottomRow);
        }

        public void UpdatePosition(int topRow, int bottomRow)
        {
            int visibleRows = topRow - bottomRow + 1;
            float y = -_bottomPositionYAfterResize + topRow - visibleRows + 1;

            _unityCameraTransform.position = _unityCameraTransform.position.WithY(y);
        }

        private void SetInitialPosition()
        {
            float x = ComputePositionX();
            const float y = 0.0f;

            _unityCameraTransform.position = _unityCameraTransform.position.WithX(x).WithY(y);
        }

        private float ComputePositionX()
        {
            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            return Mathf.Floor(0.5f * board.Columns);
        }

        private void UpdateSize(float topPositionY, float bottomPositionY)
        {
            float initialSize = _unityCamera.orthographicSize;
            float initialVisibleRows = topPositionY - bottomPositionY;

            int newVisibleRows = _camera.TopRow - _camera.BottomRow + 1;
            float newSize = initialSize / initialVisibleRows * newVisibleRows;

            _unityCamera.orthographicSize = newSize;
            _bottomPositionYAfterResize = bottomPositionY * newSize / initialSize;
        }
    }
}