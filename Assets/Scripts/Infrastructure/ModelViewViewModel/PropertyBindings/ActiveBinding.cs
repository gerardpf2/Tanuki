using UnityEngine;

namespace Infrastructure.ModelViewViewModel.PropertyBindings
{
    public class ActiveBinding : PropertyBinding<bool>
    {
        [SerializeField] private GameObject _gameObject;

        public override void Set(bool value)
        {
            _gameObject.SetActive(value);
        }
    }
}