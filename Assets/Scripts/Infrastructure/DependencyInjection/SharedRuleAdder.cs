using Infrastructure.DependencyInjection.Rules;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    // TODO: Test
    public class SharedRuleAdder : ISharedRuleAdder
    {
        private readonly IRuleAdder _sharedRuleAdder;
        private readonly IRuleFactory _ruleFactory;

        private IRuleAdder _targetRuleAdder;
        private IRuleResolver _targetRuleResolver;

        public SharedRuleAdder([NotNull] IRuleAdder sharedRuleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            _sharedRuleAdder = sharedRuleAdder;
            _ruleFactory = ruleFactory;
        }

        public void Add<T>(IRule<T> rule, object key = null) where T : class
        {
            _sharedRuleAdder.Add(_ruleFactory.GetTarget<T>(_targetRuleResolver, key), key);
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