using System;
using Infrastructure.DependencyInjection.Rules;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class RuleResolver : IRuleResolver
    {
        private readonly IRuleContainer _ruleContainer;
        private readonly IRuleResolver _parentRuleResolver;

        public RuleResolver([NotNull] IRuleContainer ruleContainer, IRuleResolver parentRuleResolver)
        {
            _ruleContainer = ruleContainer;
            _parentRuleResolver = parentRuleResolver;
        }

        public T Resolve<T>()
        {
            if (TryResolve(out T result))
            {
                return result;
            }

            throw new InvalidOperationException(); // TODO
        }

        public bool TryResolve<T>(out T result)
        {
            if (_ruleContainer.TryGet(out IRule<T> rule))
            {
                result = rule.Resolve(this);

                if (result == null)
                {
                    throw new InvalidOperationException(); // TODO
                }

                return true;
            }

            if (_parentRuleResolver != null)
            {
                return _parentRuleResolver.TryResolve(out result);
            }

            result = default;

            return false;
        }
    }
}