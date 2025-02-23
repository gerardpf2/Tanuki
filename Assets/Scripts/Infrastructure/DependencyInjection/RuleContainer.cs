using System;
using System.Collections.Generic;
using Infrastructure.DependencyInjection.Rules;

namespace Infrastructure.DependencyInjection
{
    public class RuleContainer : IRuleContainer
    {
        private readonly IDictionary<Type, IRule<object>> _rules = new Dictionary<Type, IRule<object>>();

        public void Add<T>(IRule<T> rule)
        {
            if (rule is IRule<object> ruleO && _rules.TryAdd(typeof(T), ruleO))
            {
                return;
            }

            throw new InvalidOperationException(); // TODO
        }

        public bool TryGet<T>(out IRule<T> rule)
        {
            if (_rules.TryGetValue(typeof(T), out IRule<object> ruleO) && ruleO is IRule<T> ruleT)
            {
                rule = ruleT;

                return true;
            }

            rule = null;

            return false;
        }
    }
}