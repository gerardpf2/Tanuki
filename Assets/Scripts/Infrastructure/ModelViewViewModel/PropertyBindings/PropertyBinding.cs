using UnityEngine;

namespace Infrastructure.ModelViewViewModel.PropertyBindings
{
    public abstract class PropertyBinding<T> : MonoBehaviour, IPropertyBinding<T>
    {
        [SerializeField] private ViewModel _viewModel;
        [SerializeField] private string _key;

        public string Key => _key;

        public abstract void Set(T value);

        private void Start()
        {
            _viewModel.Bind(this);
        }

        private void OnDestroy()
        {
            _viewModel.Unbind(this);
        }
    }
}