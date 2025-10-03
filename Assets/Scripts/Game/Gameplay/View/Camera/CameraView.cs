using Game.Gameplay.Camera;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Camera
{
    public class CameraView : ICameraView
    {
        [NotNull] private readonly ICamera _camera;
        [NotNull] private readonly UnityEngine.Camera _unityCamera;
        [NotNull] private readonly Transform _unityCameraTransform;

        public CameraView([NotNull] ICamera camera, [NotNull] ICameraGetter cameraGetter)
        {
            ArgumentNullException.ThrowIfNull(camera);
            ArgumentNullException.ThrowIfNull(cameraGetter);

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
            // TODO
        }

        private void UpdateSize(float topPositionY, float bottomPositionY)
        {
            // TODO
        }
    }
}