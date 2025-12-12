using Infrastructure.DependencyInjection.Rules;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class SharedRuleAdder : ISharedRuleAdder
    {
        [NotNull] private readonly IRuleAdder _ruleAdder;
        [NotNull] private readonly IRuleFactory _ruleFactory;

        private IRuleAdder _targetRuleAdder;
        private IRuleResolver _targetRuleResolver;

        public SharedRuleAdder([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            _ruleAdder = ruleAdder;
            _ruleFactory = ruleFactory;
        }

        public void Add<T>(IRule<T> rule, object key = null) where T : class
        {
            InvalidOperationException.ThrowIfNull(_targetRuleAdder);
            InvalidOperationException.ThrowIfNull(_targetRuleResolver);

            _targetRuleAdder.Add(rule, key);
            _ruleAdder.Add(_ruleFactory.GetTarget<T>(_targetRuleResolver, key), key);
        }

        public void SetTarget([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleResolver ruleResolver)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleResolver);

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