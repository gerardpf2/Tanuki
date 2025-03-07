using System;
using System.Collections.Generic;
using JetBrains.Annotations;

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
                if (EqualityComparer<T>.Default.Equals(_value, value))
                {
                    return;
                }

                _value = value;
                _listeners(_value);
            }
        }

        public BoundProperty(string key, T value)
        {
            Key = key;
            _value = value;
        }

        public void Add([NotNull] Action<T> listener)
        {
            _listeners += listener;
            listener(_value);
        }

        public void Remove(Action<T> listener)
        {
            _listeners -= listener;
        }
    }
}