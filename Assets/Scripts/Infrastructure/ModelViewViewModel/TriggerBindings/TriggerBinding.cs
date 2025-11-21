using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel.TriggerBindings
{
    public abstract class TriggerBinding<T> : MonoBehaviour, ITriggerBinding<T>
    {
        [SerializeField] private ViewModel _viewModel;
        [SerializeField] private string _key;

        public string Key => _key;

        public abstract void OnTriggered(T data);

        private void Start()
        {
            InvalidOperationException.ThrowIfNull(_viewModel);

            _viewModel.Bind(this);
        }

        private void OnDestroy()
        {
            InvalidOperationException.ThrowIfNull(_viewModel);

            _viewModel.Unbind(this);
        }
    }
}