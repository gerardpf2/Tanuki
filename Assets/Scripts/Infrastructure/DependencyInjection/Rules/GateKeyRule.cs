using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.Rules
{
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
    }
}