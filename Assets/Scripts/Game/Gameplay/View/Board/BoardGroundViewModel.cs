using Game.Gameplay.Board;
using Game.Gameplay.View.Camera;
using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity.Utils;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public class BoardGroundViewModel : ViewModel
    {
        // TODO: Use bindings

        [SerializeField] private float _extraWidth;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private IBoard _board;
        private ICameraView _cameraView;

        protected override void Awake()
        {
            base.Awake();

            InjectResolver.Resolve(this);
        }

        public void Inject([NotNull] IBoard board, [NotNull] ICameraView cameraView)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(cameraView);

            _board = board;
            _cameraView = cameraView;

            UpdatePositionX();
            UpdateWidth();
        }

        private void UpdatePositionX()
        {
            InvalidOperationException.ThrowIfNull(_spriteRenderer);
            InvalidOperationException.ThrowIfNull(_board);

            float x = 0.5f * (_board.Columns - 1);

            _spriteRenderer.transform.position = _spriteRenderer.transform.position.WithX(x);
        }

        private void UpdateWidth()
        {
            InvalidOperationException.ThrowIfNull(_spriteRenderer);
            InvalidOperationException.ThrowIfNull(_board);
            InvalidOperationException.ThrowIfNull(_cameraView);

            float width = _board.Columns + _extraWidth;
            float height = _cameraView.ExtraRowsOnBottom;

            _spriteRenderer.size = new Vector2(width, height);
        }
    }
}