using Infrastructure.DependencyInjection.Rules;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

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
            if (!TryResolve(out T result, key))
            {
                InvalidOperationException.Throw($"Cannot resolve rule with Type: {typeof(T)} and Key: {key}");
            }

            return result;
        }

        public bool TryResolve<T>(out T result, object key = null)
        {
            if (_ruleGetter.TryGet(out IRule<T> rule, key))
            {
                result = rule.Resolve(this);

                if (result != null) // "!=" instead of "is not" because of Unity's operator overloads
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