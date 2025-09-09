using Game.Gameplay.View.Board;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using Infrastructure.Unity.Utils;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Camera
{
    public class CameraController : ICameraController, ICameraBoardViewPropertiesGetter, ICameraBoardViewPropertiesSetter
    {
        private const int ExtraRowsOnTop = 5; // TODO: Scriptable object for this and other camera params

        [NotNull] private readonly IReadonlyBoardView _boardView;
        [NotNull] private readonly Transform _cameraTransform;

        private float? _topPositionY;
        private float? _bottomPositionY;

        public int VisibleTopRow
        {
            get
            {
                InvalidOperationException.ThrowIfNull(_topPositionY);

                return (int)Mathf.Floor(_cameraTransform.position.y + _topPositionY.Value) - 1;
            }
        }

        public CameraController([NotNull] IReadonlyBoardView boardView, [NotNull] ICameraGetter cameraGetter)
        {
            ArgumentNullException.ThrowIfNull(boardView);
            ArgumentNullException.ThrowIfNull(cameraGetter);

            _boardView = boardView;
            _cameraTransform = cameraGetter.GetMain().transform;
        }

        public void Initialize()
        {
            Uninitialize();

            // TODO: Cache board ref and use it to register, unregister and update position

            SetInitialPosition();

            RegisterToEvents();
        }

        public void Uninitialize()
        {
            UnregisterFromEvents();

            SetInitialPosition();

            _topPositionY = null;
            _bottomPositionY = null;
        }

        public void SetBoardViewLimits(float topPositionY, float bottomPositionY)
        {
            _topPositionY = topPositionY;
            _bottomPositionY = bottomPositionY;

            UpdatePosition();
        }

        private void RegisterToEvents()
        {
            UnregisterFromEvents();

            InvalidOperationException.ThrowIfNull(_boardView.Board);

            _boardView.Board.OnHighestNonEmptyRowUpdated += UpdatePosition;
        }

        private void UnregisterFromEvents()
        {
            if (_boardView.Board is not null)
            {
                _boardView.Board.OnHighestNonEmptyRowUpdated -= UpdatePosition;
            }
        }

        private void SetInitialPosition()
        {
            _cameraTransform.position = _cameraTransform.position.WithX(0.0f).WithY(0.0f);
        }

        private void UpdatePosition()
        {
            InvalidOperationException.ThrowIfNull(_boardView.Board);
            InvalidOperationException.ThrowIfNull(_topPositionY);
            InvalidOperationException.ThrowIfNull(_bottomPositionY);

            float x = Mathf.Floor(0.5f * _boardView.Board.Columns);

            float y =
                Mathf.Max(
                    _boardView.Board.HighestNonEmptyRow + ExtraRowsOnTop - _topPositionY.Value,
                    -_bottomPositionY.Value
                );

            _cameraTransform.position = _cameraTransform.position.WithX(x).WithY(y);
        }
    }
}