using Game.Gameplay.Board;
using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity.Utils;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public class BoardViewGroundViewModel : ViewModel
    {
        [SerializeField] private float _extraWidth;
        [SerializeField] private SpriteRenderer _spriteRenderer; // TODO: Binding

        private IBoard _board;

        protected override void Awake()
        {
            base.Awake();

            InjectResolver.Resolve(this);
        }

        public void Inject([NotNull] IBoard board)
        {
            ArgumentNullException.ThrowIfNull(board);

            _board = board;

            UpdatePosition();
            UpdateWidth();
        }

        private void UpdatePosition()
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

            const float height = 1.0f;
            float width = _board.Columns + _extraWidth;

            _spriteRenderer.size = new Vector2(width, height);
        }
    }
}