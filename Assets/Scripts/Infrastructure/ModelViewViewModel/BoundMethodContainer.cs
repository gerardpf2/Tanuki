using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Infrastructure.ModelViewViewModel
{
    public class BoundMethodContainer : IBoundMethodContainer
    {
        [NotNull] private readonly IDictionary<string, IBoundMethod> _boundMethods = new Dictionary<string, IBoundMethod>();

        public void Add([NotNull] IBoundMethod boundMethod)
        {
            ArgumentNullException.ThrowIfNull(boundMethod);

            if (boundMethod.Key is not null && _boundMethods.TryAdd(boundMethod.Key, boundMethod))
            {
                return;
            }

            InvalidOperationException.Throw($"Cannot add bound method with Key: {boundMethod.Key}");
        }

        public IBoundMethod Get([NotNull] string key)
        {
            ArgumentNullException.ThrowIfNull(key);

            if (_boundMethods.TryGetValue(key, out IBoundMethod boundMethod))
            {
                return boundMethod;
            }

            InvalidOperationException.Throw($"Cannot get bound method with Key: {key}");
        }
    }
}