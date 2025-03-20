using System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.ModelViewViewModel
{
    public class BoundMethod : IBoundMethod
    {
        [NotNull] private readonly Action _method;

        public string Key { get; }

        public BoundMethod([NotNull] Action method) : this(method.Method.Name, method) { }

        public BoundMethod(string key, [NotNull] Action method)
        {
            ArgumentNullException.ThrowIfNull(method);

            Key = key;
            _method = method;
        }

        public void Call()
        {
            _method();
        }
    }
}