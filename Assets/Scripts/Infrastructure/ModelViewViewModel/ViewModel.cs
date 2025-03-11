using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel.MethodBindings;
using Infrastructure.ModelViewViewModel.PropertyBindings;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel
{
    public class ViewModel : MonoBehaviour
    {
        private IBoundPropertyContainer _boundPropertyContainer;
        private IBoundMethodContainer _boundMethodContainer;

        protected virtual void Awake()
        {
            InjectResolver.Resolve(this);
        }

        public void Inject(
            [NotNull] IBoundPropertyContainer boundPropertyContainer,
            [NotNull] IBoundMethodContainer boundMethodContainer)
        {
            _boundPropertyContainer = boundPropertyContainer;
            _boundMethodContainer = boundMethodContainer;
        }

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