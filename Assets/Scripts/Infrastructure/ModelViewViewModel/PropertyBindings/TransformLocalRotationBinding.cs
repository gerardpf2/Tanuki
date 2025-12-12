using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel.PropertyBindings
{
    public class TransformLocalRotationBinding : PropertyBinding<Quaternion>
    {
        [SerializeField] private Transform _transform;

        public override void Set(Quaternion value)
        {
            InvalidOperationException.ThrowIfNull(_transform);

            _transform.rotation = value;
        }
    }
}