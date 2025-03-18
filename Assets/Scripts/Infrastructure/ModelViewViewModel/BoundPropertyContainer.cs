using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.ModelViewViewModel
{
    public class BoundPropertyContainer : IBoundPropertyContainer
    {
        [NotNull] private readonly IDictionary<string, object> _boundProperties = new Dictionary<string, object>();

        public void Add<T>([NotNull] IBoundProperty<T> boundProperty)
        {
            ArgumentNullException.ThrowIfNull(boundProperty);

            if (boundProperty.Key is not null && _boundProperties.TryAdd(boundProperty.Key, boundProperty))
            {
                return;
            }

            throw new InvalidOperationException($"Cannot add bound property with Type: {typeof(T)} and Key: {boundProperty.Key}");
        }

        public IBoundProperty<T> Get<T>([NotNull] string key)
        {
            ArgumentNullException.ThrowIfNull(key);

            if (_boundProperties.TryGetValue(key, out object obj) && obj is IBoundProperty<T> boundProperty)
            {
                return boundProperty;
            }

            throw new InvalidOperationException($"Cannot get bound property with Type: {typeof(T)} and Key: {key}");
        }
    }
}