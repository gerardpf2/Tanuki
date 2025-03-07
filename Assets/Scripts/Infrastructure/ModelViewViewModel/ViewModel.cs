using Infrastructure.ModelViewViewModel.MethodBindings;
using Infrastructure.ModelViewViewModel.PropertyBindings;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel
{
    public class ViewModel : MonoBehaviour
    {
        private readonly IBoundPropertyContainer _boundPropertyContainer = new BoundPropertyContainer();
        private readonly IBoundMethodContainer _boundMethodContainer = new BoundMethodContainer();

        public void Bind<T>([NotNull] IPropertyBinding<T> propertyBinding)
        {
            _boundPropertyContainer.Get<T>(propertyBinding.Key).Add(propertyBinding.Set);
        }

        public void Unbind<T>([NotNull] IPropertyBinding<T> propertyBinding)
        {
            _boundPropertyContainer.Get<T>(propertyBinding.Key).Remove(propertyBinding.Set);
        }

        public void Resolve([NotNull] IMethodBinding methodBinding)
        {
            _boundMethodContainer.Get(methodBinding.Key).Call();
        }

        protected void Add<T>(IBoundProperty<T> boundProperty)
        {
            _boundPropertyContainer.Add(boundProperty);
        }

        protected void Add(IBoundMethod boundMethod)
        {
            _boundMethodContainer.Add(boundMethod);
        }
    }
}