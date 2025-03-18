using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel.MethodBindings;
using Infrastructure.ModelViewViewModel.PropertyBindings;
using Infrastructure.System.Exceptions;
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
            ArgumentNullException.ThrowIfNull(boundPropertyContainer);
            ArgumentNullException.ThrowIfNull(boundMethodContainer);

            _boundPropertyContainer = boundPropertyContainer;
            _boundMethodContainer = boundMethodContainer;
        }

        public void Bind<T>([NotNull] IPropertyBinding<T> propertyBinding)
        {
            ArgumentNullException.ThrowIfNull(propertyBinding);
            InvalidOperationException.ThrowIfNull(_boundPropertyContainer);

            _boundPropertyContainer.Get<T>(propertyBinding.Key).Add(propertyBinding.Set);
        }

        public void Unbind<T>([NotNull] IPropertyBinding<T> propertyBinding)
        {
            ArgumentNullException.ThrowIfNull(propertyBinding);
            InvalidOperationException.ThrowIfNull(_boundPropertyContainer);

            _boundPropertyContainer.Get<T>(propertyBinding.Key).Remove(propertyBinding.Set);
        }

        public void Resolve([NotNull] IMethodBinding methodBinding)
        {
            ArgumentNullException.ThrowIfNull(methodBinding);
            InvalidOperationException.ThrowIfNull(_boundMethodContainer);

            _boundMethodContainer.Get(methodBinding.Key).Call();
        }

        protected void Add<T>(IBoundProperty<T> boundProperty)
        {
            InvalidOperationException.ThrowIfNull(_boundPropertyContainer);

            _boundPropertyContainer.Add(boundProperty);
        }

        protected void Add(IBoundMethod boundMethod)
        {
            InvalidOperationException.ThrowIfNull(_boundMethodContainer);

            _boundMethodContainer.Add(boundMethod);
        }
    }
}