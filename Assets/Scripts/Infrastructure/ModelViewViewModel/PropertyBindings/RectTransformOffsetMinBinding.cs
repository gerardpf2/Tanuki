using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel.PropertyBindings
{
    public class RectTransformOffsetMinBinding : PropertyBinding<Vector2>
    {
        [SerializeField] private RectTransform _rectTransform;

        public override void Set(Vector2 value)
        {
            InvalidOperationException.ThrowIfNull(_rectTransform);

            _rectTransform.offsetMin = value;
        }
    }
}