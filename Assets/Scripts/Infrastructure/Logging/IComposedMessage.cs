using System.Collections.Generic;
using JetBrains.Annotations;

namespace Infrastructure.Logging
{
    public interface IComposedMessage
    {
        [NotNull]
        IEnumerable<KeyValuePair<string, string>> Dimensions { get; }

        string Body { get; set; }

        void AddDimension(string key, string value);
    }
}