using Infrastructure.ModelViewViewModel.PropertyBindings;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel
{
    public class ViewModel : MonoBehaviour
    {
        private readonly IBoundPropertyContainer _boundPropertyContainer = new BoundPropertyContainer();

        public void Bind<T>([NotNull] IPropertyBinding<T> propertyBinding)
        {
            _boundPropertyContainer.Get<T>(propertyBinding.Key).Add(propertyBinding.Set);
        }

        public void Unbind<T>([NotNull] IPropertyBinding<T> propertyBinding)
        {
            _boundPropertyContainer.Get<T>(propertyBinding.Key).Remove(propertyBinding.Set);
        }

        protected void Add<T>(IBoundProperty<T> boundProperty)
        {
            _boundPropertyContainer.Add(boundProperty);
        }
    }
}