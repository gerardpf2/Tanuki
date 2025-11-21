using Infrastructure.ModelViewViewModel.MethodBindings;
using Infrastructure.ModelViewViewModel.PropertyBindings;
using Infrastructure.ModelViewViewModel.TriggerBindings;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel
{
    public class ViewModel : MonoBehaviour
    {
        [NotNull] private readonly IBoundMethodContainer _boundMethodContainer = new BoundMethodContainer();
        [NotNull] private readonly IBoundPropertyContainer _boundPropertyContainer = new BoundPropertyContainer();
        [NotNull] private readonly IBoundTriggerContainer _boundTriggerContainer = new BoundTriggerContainer();

        #region Method binding

        public void Resolve([NotNull] IMethodBinding methodBinding)
        {
            ArgumentNullException.ThrowIfNull(methodBinding);

            _boundMethodContainer.Get(methodBinding.Key).Call();
        }

        protected void Add(IBoundMethod boundMethod)
        {
            _boundMethodContainer.Add(boundMethod);
        }

        #endregion

        #region Property binding

        public void Bind<T>([NotNull] IPropertyBinding<T> propertyBinding)
        {
            ArgumentNullException.ThrowIfNull(propertyBinding);

            _boundPropertyContainer.Get<T>(propertyBinding.Key).Add(propertyBinding.Set);
        }

        public void Unbind<T>([NotNull] IPropertyBinding<T> propertyBinding)
        {
            ArgumentNullException.ThrowIfNull(propertyBinding);

            _boundPropertyContainer.Get<T>(propertyBinding.Key).Remove(propertyBinding.Set);
        }

        protected void Add<T>(IBoundProperty<T> boundProperty)
        {
            _boundPropertyContainer.Add(boundProperty);
        }

        #endregion

        #region Trigger binding

        public void Bind<T>([NotNull] ITriggerBinding<T> triggerBinding)
        {
            ArgumentNullException.ThrowIfNull(triggerBinding);

            _boundTriggerContainer.Get<T>(triggerBinding.Key).Add(triggerBinding.OnTriggered);
        }

        public void Unbind<T>([NotNull] ITriggerBinding<T> triggerBinding)
        {
            ArgumentNullException.ThrowIfNull(triggerBinding);

            _boundTriggerContainer.Get<T>(triggerBinding.Key).Remove(triggerBinding.OnTriggered);
        }

        protected void Add<T>(IBoundTrigger<T> boundTrigger)
        {
            _boundTriggerContainer.Add(boundTrigger);
        }

        #endregion
    }
}