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
            // TODO: Cache prev unity camera values
        }

        public void Uninitialize()
        {
            // TODO: Restore prev unity camera values
        }

        public void SetBoardViewLimits(float topPositionY, float bottomPositionY)
        {
            UpdateSize(topPositionY, bottomPositionY);
            UpdatePosition(_camera.TopRow, _camera.BottomRow);
        }

        public void UpdatePosition(int topRow, int bottomRow)
        {
            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            float x = Mathf.Floor(0.5f * board.Columns);
            float y = 0.5f * (topRow - bottomRow + 1);

            _unityCameraTransform.position = _unityCameraTransform.position.WithX(x).WithY(y);
        }

        private void UpdateSize(float topPositionY, float bottomPositionY)
        {
            // Assumes orthographic camera, etc If other game modes modify camera params other than size, this class should be reviewed
            // Top and bottom board view positions are based on orthographic camera size

            float defaultSize = _unityCamera.orthographicSize;
            float defaultVisibleRows = topPositionY - bottomPositionY;

            int visibleRows = _camera.TopRow - _camera.BottomRow + 1;

            _unityCamera.orthographicSize = defaultSize * visibleRows / defaultVisibleRows;
        }
    }
}