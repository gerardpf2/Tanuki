using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

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

            InvalidOperationException.Throw($"Cannot add bound property with Type: {typeof(T)} and Key: {boundProperty.Key}");
        }

        public IBoundProperty<T> Get<T>([NotNull] string key)
        {
            ArgumentNullException.ThrowIfNull(key);

            if (_boundProperties.TryGetValue(key, out object obj) && obj is IBoundProperty<T> boundProperty)
            {
                return boundProperty;
            }

            InvalidOperationException.Throw($"Cannot get bound property with Type: {typeof(T)} and Key: {key}");
        }
    }
}