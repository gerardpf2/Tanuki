using System;

namespace Infrastructure.ModelViewViewModel
{
    public interface IBoundProperty<T>
    {
        string Key { get; }

        T Value { get; set; }

        void Add(Action<T> listener);

        void Remove(Action<T> listener);
    }
}