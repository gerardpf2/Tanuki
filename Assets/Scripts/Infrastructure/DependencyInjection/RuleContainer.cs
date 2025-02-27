using System;
using System.Collections.Generic;
using Infrastructure.DependencyInjection.Rules;

namespace Infrastructure.DependencyInjection
{
    public class RuleContainer : IRuleAdder, IRuleGetter
    {
        private readonly IDictionary<(Type, object), IRule<object>> _rules = new Dictionary<(Type, object), IRule<object>>();

        public void Add<T>(IRule<T> rule, object key = null)
        {
            if (rule is IRule<object> ruleO && _rules.TryAdd((typeof(T), key), ruleO))
            {
                return;
            }

            throw new InvalidOperationException(); // TODO
        }

        public bool TryGet<T>(out IRule<T> rule, object key = null)
        {
            if (_rules.TryGetValue((typeof(T), key), out IRule<object> ruleO) && ruleO is IRule<T> ruleT)
            {
                rule = ruleT;

                return true;
            }

            rule = null;

            return false;
        }
    }
}