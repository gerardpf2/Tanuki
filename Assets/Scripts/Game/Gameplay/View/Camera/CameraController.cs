using Game.Gameplay.Board;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using Infrastructure.Unity.Utils;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Camera
{
    public class CameraController : ICameraController, ICameraBoardViewGetter, ICameraBoardViewSetter
    {
        private const int ExtraRowsOnTop = 5; // TODO: Scriptable object for this and other camera params

        [NotNull] private readonly Transform _cameraTransform;

        private IReadonlyBoard _board;
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

        public int Column => (int)Mathf.Floor(_cameraTransform.position.x);

        public CameraController([NotNull] ICameraGetter cameraGetter)
        {
            ArgumentNullException.ThrowIfNull(cameraGetter);

            _cameraTransform = cameraGetter.GetMain().transform;
        }

        public void Initialize([NotNull] IReadonlyBoard board)
        {
            // TODO: Check allow multiple Initialize. Add Clear ¿?

            ArgumentNullException.ThrowIfNull(board);

            _board = board;
        }

        public void SetBoardViewLimits(float topPositionY, float bottomPositionY)
        {
            _topPositionY = topPositionY;
            _bottomPositionY = bottomPositionY;

            MoveToHighestNonEmptyRow(); // TODO
        }

        private void MoveToHighestNonEmptyRow()
        {
            InvalidOperationException.ThrowIfNull(_board);
            InvalidOperationException.ThrowIfNull(_topPositionY);
            InvalidOperationException.ThrowIfNull(_bottomPositionY);

            float x = Mathf.Floor(0.5f * _board.Columns);

            float y =
                Mathf.Max(
                    _board.HighestNonEmptyRow + ExtraRowsOnTop - _topPositionY.Value,
                    -_bottomPositionY.Value
                );

            _cameraTransform.position = _cameraTransform.position.WithX(x).WithY(y);
        }
    }
}