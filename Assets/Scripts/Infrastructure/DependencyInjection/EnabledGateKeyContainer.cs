using System.Collections.Generic;

namespace Infrastructure.DependencyInjection
{
    public class EnabledGateKeyContainer : IEnabledGateKeyAdder, IEnabledGateKeyGetter
    {
        private readonly ICollection<string> _gateKeys = new HashSet<string> { null };

        public void Add(string gateKey)
        {
            _gateKeys.Add(gateKey);
        }

        public bool Contains(string gateKey)
        {
            return _gateKeys.Contains(gateKey);
        }
    }
}