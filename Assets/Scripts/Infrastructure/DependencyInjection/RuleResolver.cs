using System;
using Infrastructure.DependencyInjection.Rules;

namespace Infrastructure.DependencyInjection
{
    public class RuleResolver : IRuleResolver
    {
        // TODO: Test private vs public

        private readonly IRuleGetter _privateRuleGetter;
        private readonly IRuleGetter _publicRuleGetter;
        private readonly IRuleResolver _parentRuleResolver;

        public RuleResolver(
            IRuleGetter privateRuleGetter,
            IRuleGetter publicRuleGetter,
            IRuleResolver parentRuleResolver)
        {
            _privateRuleGetter = privateRuleGetter;
            _publicRuleGetter = publicRuleGetter;
            _parentRuleResolver = parentRuleResolver;
        }

        public T Resolve<T>(IRuleResolver sourceRuleResolver, object key = null)
        {
            if (TryResolve(sourceRuleResolver, out T result, key))
            {
                return result;
            }

            throw new InvalidOperationException($"Cannot resolve rule with Type: {typeof(T)} and Key: {key}");
        }

        public bool TryResolve<T>(IRuleResolver sourceRuleResolver, out T result, object key = null)
        {
            if (sourceRuleResolver == this && TryResolve(_privateRuleGetter, key, out result))
            {
                return true;
            }

            if (TryResolve(_publicRuleGetter, key, out result))
            {
                return true;
            }

            if (_parentRuleResolver != null)
            {
                return _parentRuleResolver.TryResolve(sourceRuleResolver, out result, key);
            }

            result = default;

            return false;
        }

        private bool TryResolve<T>(IRuleGetter ruleGetter, object key, out T result)
        {
            if (ruleGetter != null && ruleGetter.TryGet(out IRule<T> rule, key))
            {
                result = rule.Resolve(this);

                return result != null;
            }

            result = default;

            return false;
        }
    }
}