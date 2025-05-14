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
            // TODO: Check allow multiple Initialize. Add Clear ¿?
        }

        public void SetBoardViewLimits(float topPositionY, float bottomPositionY)
        {
            _topPositionY = topPositionY;
            _bottomPositionY = bottomPositionY;

            MoveToHighestNonEmptyRow(); // TODO
        }

        private void MoveToHighestNonEmptyRow()
        {
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