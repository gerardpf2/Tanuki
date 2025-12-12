using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Infrastructure.ModelViewViewModel
{
    // TODO: Test
    public class BoundTriggerContainer : IBoundTriggerContainer
    {
        [NotNull] private readonly IDictionary<string, object> _boundTriggers = new Dictionary<string, object>();

        public void Add<T>([NotNull] IBoundTrigger<T> boundTrigger)
        {
            ArgumentNullException.ThrowIfNull(boundTrigger);

            if (boundTrigger.Key is null || !_boundTriggers.TryAdd(boundTrigger.Key, boundTrigger))
            {
                InvalidOperationException.Throw(
                    $"Cannot add bound trigger with Type: {typeof(T)} and Key: {boundTrigger.Key}"
                );
            }
        }

        public IBoundTrigger<T> Get<T>([NotNull] string key)
        {
            ArgumentNullException.ThrowIfNull(key);

            if (_boundTriggers.TryGetValue(key, out object obj) && obj is IBoundTrigger<T> boundTrigger)
            {
                return boundTrigger;
            }

            InvalidOperationException.Throw($"Cannot get bound trigger with Type: {typeof(T)} and Key: {key}");

            return null;
        }
    }
}