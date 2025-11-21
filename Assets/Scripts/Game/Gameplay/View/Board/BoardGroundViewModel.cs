using Game.Gameplay.Board;
using Game.Gameplay.View.Camera;
using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public class BoardGroundViewModel : ViewModel
    {
        [SerializeField] private float _extraWidth;

        [NotNull] private readonly IBoundProperty<Vector3> _position = new BoundProperty<Vector3>("Position");
        [NotNull] private readonly IBoundProperty<Vector2> _size = new BoundProperty<Vector2>("Size");

        private IBoard _board;
        private ICameraView _cameraView;

        private void Awake()
        {
            InjectResolver.Resolve(this);

            Add(_position);
            Add(_size);

            UpdatePosition();
            UpdateSize();
        }

        public void Inject([NotNull] IBoard board, [NotNull] ICameraView cameraView)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(cameraView);

            _board = board;
            _cameraView = cameraView;
        }

        private void UpdatePosition()
        {
            InvalidOperationException.ThrowIfNull(_board);

            const float y = 0.0f;
            const float z = 0.0f;

            float x = 0.5f * (_board.Columns - 1);

            _position.Value = new Vector3(x, y, z);
        }

        private void UpdateSize()
        {
            InvalidOperationException.ThrowIfNull(_board);
            InvalidOperationException.ThrowIfNull(_cameraView);

            float width = _board.Columns + _extraWidth;
            float height = _cameraView.ExtraRowsOnBottom;

            _size.Value = new Vector2(width, height);
        }
    }
}