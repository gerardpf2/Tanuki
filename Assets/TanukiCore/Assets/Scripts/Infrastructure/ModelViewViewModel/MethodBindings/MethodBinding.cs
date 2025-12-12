using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel.MethodBindings
{
    public class MethodBinding : MonoBehaviour, IMethodBinding
    {
        [SerializeField] private ViewModel _viewModel;
        [SerializeField] private string _key;

        public string Key => _key;

        protected void Call()
        {
            InvalidOperationException.ThrowIfNull(_viewModel);

            _viewModel.Resolve(this);
        }
    }
}