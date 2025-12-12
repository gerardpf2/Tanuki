using System;

namespace Infrastructure.ModelViewViewModel
{
    public interface IBoundTrigger<T>
    {
        string Key { get; }

        void Trigger(T data);
        
        void Add(Action<T> listener);

        void Remove(Action<T> listener);
    }
}