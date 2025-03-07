using System;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.Rules
{
    // Gate key is expected to be managed at scope composer level, but it is also possible to manage it at rule level
    // using GateKeyRule. GateKeyRule wraps another rule and only resolves it if the gate key is enabled

    public class GateKeyRule<T> : IRule<T> where T : class
    {
        private readonly IEnabledGateKeyGetter _enabledGateKeyGetter;
        private readonly IRule<T> _rule;
        private readonly object _gateKey;

        public GateKeyRule(
            [NotNull] IEnabledGateKeyGetter enabledGateKeyGetter,
            [NotNull] IRule<T> rule,
            object gateKey)
        {
            _enabledGateKeyGetter = enabledGateKeyGetter;
            _rule = rule;
            _gateKey = gateKey;
        }

        public T Resolve(IRuleResolver ruleResolver)
        {
            return _enabledGateKeyGetter.Contains(_gateKey) ? _rule.Resolve(ruleResolver) : null;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is not GateKeyRule<T> other)
            {
                return false;
            }

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_enabledGateKeyGetter, _rule, _gateKey);
        }

        protected bool Equals(GateKeyRule<T> other)
        {
            return
                Equals(_enabledGateKeyGetter, other._enabledGateKeyGetter) &&
                Equals(_rule, other._rule) &&
                Equals(_gateKey, other._gateKey);
        }
    }
}