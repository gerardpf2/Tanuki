using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel.PropertyBindings
{
    public class ActiveBinding : PropertyBinding<bool>
    {
        [SerializeField] private GameObject _gameObject;

        public override void Set(bool value)
        {
            InvalidOperationException.ThrowIfNull(_gameObject);

            _gameObject.SetActive(value);
        }
    }
}