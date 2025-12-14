using System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.ModelViewViewModel
{
    // TODO: Test
    public class BoundTrigger<T> : IBoundTrigger<T>
    {
        private Action<T> _listeners;

        public string Key { get; }

        public BoundTrigger(string key)
        {
            Key = key;
        }

        public void Trigger(T data)
        {
            _listeners?.Invoke(data);
        }

        public void Add([NotNull] Action<T> listener)
        {
            ArgumentNullException.ThrowIfNull(listener);

            _listeners += listener;
        }

        public void Remove(Action<T> listener)
        {
            _listeners -= listener;
        }
    }
}