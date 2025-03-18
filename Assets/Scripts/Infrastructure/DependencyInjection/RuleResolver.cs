using System;
using Infrastructure.DependencyInjection.Rules;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.DependencyInjection
{
    public class RuleResolver : IRuleResolver
    {
        [NotNull] private readonly IRuleGetter _ruleGetter;
        private readonly IRuleResolver _parentRuleResolver;

        public RuleResolver([NotNull] IRuleGetter ruleGetter, IRuleResolver parentRuleResolver)
        {
            ArgumentNullException.ThrowIfNull(ruleGetter);

            _ruleGetter = ruleGetter;
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
            if (_ruleGetter.TryGet(out IRule<T> rule, key))
            {
                result = rule.Resolve(this);

                if (result is not null)
                {
                    return true;
                }
            }

            if (_parentRuleResolver is not null)
            {
                return _parentRuleResolver.TryResolve(out result, key);
            }

            result = default;

            return false;
        }
    }
}