using System;
using Infrastructure.Gating;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.Rules
{
    // Gate key is expected to be managed at scope composer level, but it is also possible to manage it at rule level
    // using GateKeyRule. GateKeyRule wraps another rule and only resolves it if the gate key is enabled

    public class GateKeyRule<T> : IRule<T> where T : class
    {
        private readonly IGateValidator _gateValidator;
        private readonly IRule<T> _rule;
        private readonly string _gateKey;

        public GateKeyRule([NotNull] IGateValidator gateValidator, [NotNull] IRule<T> rule, string gateKey)
        {
            _gateValidator = gateValidator;
            _rule = rule;
            _gateKey = gateKey;
        }

        public T Resolve(IRuleResolver ruleResolver)
        {
            return _gateValidator.Validate(_gateKey) ? _rule.Resolve(ruleResolver) : null;
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
            return HashCode.Combine(_gateValidator, _rule, _gateKey);
        }

        protected bool Equals([NotNull] GateKeyRule<T> other)
        {
            return
                Equals(_gateValidator, other._gateValidator) &&
                Equals(_rule, other._rule) &&
                Equals(_gateKey, other._gateKey);
        }
    }
}