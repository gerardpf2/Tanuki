using System;
using Infrastructure.DependencyInjection.Rules;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class RuleResolver : IRuleResolver
    {
        private readonly IRuleGetter _publicRuleGetter;
        private readonly IRuleResolver _parentRuleResolver;

        public RuleResolver([NotNull] IRuleGetter publicRuleGetter, IRuleResolver parentRuleResolver)
        {
            _publicRuleGetter = publicRuleGetter;
            _parentRuleResolver = parentRuleResolver;
        }

        public T Resolve<T>(object key = null)
        {
            if (TryResolve(out T result, key))
            {
                return result;
            }

            throw new InvalidOperationException($"Cannot resolve rule with Type: {typeof(T)} and Key: {key}");
        }

        public bool TryResolve<T>(out T result, object key = null)
        {
            if (_publicRuleGetter.TryGet(out IRule<T> rule, key))
            {
                result = rule.Resolve(this);

                if (result != null)
                {
                    return true;
                }
            }

            if (_parentRuleResolver != null)
            {
                return _parentRuleResolver.TryResolve(out result, key);
            }

            result = default;

            return false;
        }
    }
}