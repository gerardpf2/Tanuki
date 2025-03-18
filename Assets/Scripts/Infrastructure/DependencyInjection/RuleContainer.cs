using System;
using System.Collections.Generic;
using Infrastructure.DependencyInjection.Rules;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class RuleContainer : IRuleAdder, IRuleGetter
    {
        [NotNull] private readonly IDictionary<(Type, object), IRule<object>> _rules = new Dictionary<(Type, object), IRule<object>>();

        public void Add<T>(IRule<T> rule, object key = null) where T : class
        {
            if (_rules.TryAdd((typeof(T), key), rule))
            {
                return;
            }

            throw new InvalidOperationException($"Cannot add rule with Type: {typeof(T)} and Key: {key}");
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