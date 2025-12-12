using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel.PropertyBindings
{
    public class TransformLocalPositionBinding : PropertyBinding<Vector3>
    {
        [SerializeField] private Transform _transform;

        public override void Set(Vector3 value)
        {
            InvalidOperationException.ThrowIfNull(_transform);

            _transform.localPosition = value;
        }
    }
}