using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel.PropertyBindings
{
    public class SpriteRendererSizeBinding : PropertyBinding<Vector2>
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public override void Set(Vector2 value)
        {
            InvalidOperationException.ThrowIfNull(_spriteRenderer);

            _spriteRenderer.size = value;
        }
    }
}