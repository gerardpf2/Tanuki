using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Infrastructure.Logging
{
    public class ComposedMessage : IComposedMessage
    {
        [NotNull] private readonly IDictionary<string, string> _dimensions = new Dictionary<string, string>();

        public IEnumerable<KeyValuePair<string, string>> Dimensions => _dimensions;

        public string Body { get; set; }

        public void AddDimension([NotNull] string key, string value)
        {
            ArgumentNullException.ThrowIfNull(key);

            if (!_dimensions.TryAdd(key, value))
            {
                InvalidOperationException.Throw($"Cannot add dimension with Key: {key} and Value: {value}");
            }
        }
    }
}