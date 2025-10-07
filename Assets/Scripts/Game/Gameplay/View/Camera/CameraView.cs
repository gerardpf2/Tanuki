using Game.Common;
using Game.Gameplay.Board;
using Game.Gameplay.Camera;
using Game.Gameplay.Common;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using Infrastructure.Unity.Utils;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Camera
{
    public class CameraView : ICameraView
    {
        private const int InitialRow = 0;

        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly ICamera _camera;
        [NotNull] private readonly IWorldPositionGetter _worldPositionGetter;
        [NotNull] private readonly UnityEngine.Camera _unityCamera;
        [NotNull] private readonly Transform _unityCameraTransform;

        private InitializedLabel _initializedLabel;

        private float _bottomPositionYAfterResize;

        public CameraView(
            [NotNull] IBoardContainer boardContainer,
            [NotNull] ICamera camera,
            [NotNull] IWorldPositionGetter worldPositionGetter,
            [NotNull] ICameraGetter cameraGetter)
        {
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(camera);
            ArgumentNullException.ThrowIfNull(worldPositionGetter);
            ArgumentNullException.ThrowIfNull(cameraGetter);

            _boardContainer = boardContainer;
            _camera = camera;
            _worldPositionGetter = worldPositionGetter;
            _unityCamera = cameraGetter.GetMain();
            _unityCameraTransform = _unityCamera.transform;
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

            _bottomPositionYAfterResize = 0.0f;
        }

        public void SetBoardViewLimits(float topPositionY, float bottomPositionY)
        {
            UpdateSize(topPositionY, bottomPositionY);
            UpdatePositionY(_camera.TopRow);
        }

        public void UpdatePositionY(int row)
        {
            float y = _worldPositionGetter.GetY(row + 1 - _camera.VisibleRows) - _bottomPositionYAfterResize;

            _unityCameraTransform.position = _unityCameraTransform.position.WithY(y);
        }

        private void SetInitialPosition()
        {
            int initialColumn = ComputeInitialColumn();

            float x = _worldPositionGetter.GetX(initialColumn);
            float y = _worldPositionGetter.GetY(InitialRow);

            _unityCameraTransform.position = _unityCameraTransform.position.WithX(x).WithY(y);
        }

        private int ComputeInitialColumn()
        {
            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            return Mathf.FloorToInt(0.5f * board.Columns);
        }

        private void UpdateSize(float topPositionY, float bottomPositionY)
        {
            float initialY = _worldPositionGetter.GetY(InitialRow);

            topPositionY -= initialY;
            bottomPositionY -= initialY;

            float initialSize = _unityCamera.orthographicSize;
            float initialVisibleRows = topPositionY - bottomPositionY;

            int newVisibleRows = _camera.VisibleRows;
            float newSize = initialSize / initialVisibleRows * newVisibleRows;

            _unityCamera.orthographicSize = newSize;
            _bottomPositionYAfterResize = bottomPositionY * newSize / initialSize;
        }
    }
}