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
        [NotNull] private readonly Transform _unityCameraTransform;

        private InitializedLabel _initializedLabel;

        public UnityEngine.Camera UnityCamera { get; }

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

            UnityCamera = cameraGetter.GetMain();

            _boardContainer = boardContainer;
            _camera = camera;
            _worldPositionGetter = worldPositionGetter;
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

            float initialY = _worldPositionGetter.GetY(InitialRow);
            float offsetY = (bottomPositionY - initialY) * newSize / initialSize;
            float y = _unityCameraTransform.position.y - offsetY;

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
    }
}