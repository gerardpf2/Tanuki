using System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.ModelViewViewModel
{
    public class BoundProperty<T> : IBoundProperty<T>
    {
        private Action<T> _listeners;
        private T _value;

        public string Key { get; }

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                _listeners?.Invoke(_value);
            }
        }

        public BoundProperty(string key, T value = default)
        {
            Key = key;
            _value = value;
        }

        public void Add([NotNull] Action<T> listener)
        {
            ArgumentNullException.ThrowIfNull(listener);

            _listeners += listener;
            listener(_value);
        }

        public void Remove(Action<T> listener)
        {
            _listeners -= listener;
        }
    }
}