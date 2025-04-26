using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel.PropertyBindings
{
    public abstract class SetDataBinding<T> : PropertyBinding<T>
    {
        [SerializeField] private GameObject _gameObject;

        public override void Set(T value)
        {
            InvalidOperationException.ThrowIfNull(_gameObject);

            IDataSettable<T> dataSettable = _gameObject.GetComponent<IDataSettable<T>>();

            InvalidOperationException.ThrowIfNullWithMessage(
                dataSettable,
                $"Cannot set data of Type: {typeof(T)} to game object with Name: {_gameObject.name}"
            );

            dataSettable.SetData(value);
        }
    }
}