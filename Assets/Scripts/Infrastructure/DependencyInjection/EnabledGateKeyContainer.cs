using System.Collections.Generic;

namespace Infrastructure.DependencyInjection
{
    public class EnabledGateKeyContainer : IEnabledGateKeyAdder, IEnabledGateKeyGetter
    {
        private readonly ICollection<object> _gateKeys = new HashSet<object> { null };

        public void Add(object gateKey)
        {
            _gateKeys.Add(gateKey);
        }

        public bool Contains(object gateKey)
        {
            return _gateKeys.Contains(gateKey);
        }
    }
}