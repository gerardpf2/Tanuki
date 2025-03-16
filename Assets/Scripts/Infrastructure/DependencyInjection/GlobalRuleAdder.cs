using Infrastructure.DependencyInjection.Rules;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class GlobalRuleAdder : IGlobalRuleAdder
    {
        private readonly IRuleAdder _ruleAdder;
        private readonly IRuleFactory _ruleFactory;

        private IRuleAdder _targetRuleAdder;
        private IRuleResolver _targetRuleResolver;

        public GlobalRuleAdder([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            _ruleAdder = ruleAdder;
            _ruleFactory = ruleFactory;
        }

        public void Add<T>(IRule<T> rule, object key = null) where T : class
        {
            _ruleAdder.Add(_ruleFactory.GetTarget<T>(_targetRuleResolver, key), key);
            _targetRuleAdder.Add(rule, key);
        }

        public void SetTarget([NotNull] IRuleAdder ruleAdder, IRuleResolver ruleResolver)
        {
            _targetRuleAdder = ruleAdder;
            _targetRuleResolver = ruleResolver;
        }

        public void ClearTarget()
        {
            _targetRuleAdder = null;
            _targetRuleResolver = null;
        }
    }
}