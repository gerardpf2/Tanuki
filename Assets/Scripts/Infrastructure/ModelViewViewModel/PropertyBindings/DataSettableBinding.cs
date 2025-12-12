using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel.PropertyBindings
{
    public abstract class DataSettableBinding<T> : PropertyBinding<T>
    {
        [SerializeField] private GameObject _gameObject;

        public override void Set(T value)
        {
            InvalidOperationException.ThrowIfNull(_gameObject);

            IDataSettable<T> dataSettable = _gameObject.GetComponent<IDataSettable<T>>();

            InvalidOperationException.ThrowIfNull(dataSettable);

            dataSettable.SetData(value);
        }
    }
}