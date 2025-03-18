using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

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

            throw new InvalidOperationException($"Cannot add bound method with Key: {boundMethod.Key}");
        }

        public IBoundMethod Get([NotNull] string key)
        {
            ArgumentNullException.ThrowIfNull(key);

            if (_boundMethods.TryGetValue(key, out IBoundMethod boundMethod))
            {
                return boundMethod;
            }

            throw new InvalidOperationException($"Cannot get bound method with Key: {key}");
        }
    }
}