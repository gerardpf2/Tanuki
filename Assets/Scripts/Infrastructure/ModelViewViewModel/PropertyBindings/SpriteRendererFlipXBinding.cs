using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel.PropertyBindings
{
    public class SpriteRendererFlipXBinding : PropertyBinding<bool>
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public override void Set(bool value)
        {
            InvalidOperationException.ThrowIfNull(_spriteRenderer);

            _spriteRenderer.flipX = value;
        }
    }
}