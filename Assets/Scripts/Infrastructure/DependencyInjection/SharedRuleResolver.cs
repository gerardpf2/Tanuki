using System;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    // TODO: Test
    public class SharedRuleResolver : RuleResolver
    {
        public static SharedRuleResolver Instance { get; private set; }

        public SharedRuleResolver([NotNull] IRuleGetter ruleGetter) : base(ruleGetter, null)
        {
            if (Instance != null)
            {
                throw new InvalidOperationException("Cannot be constructed more than once");
            }

            Instance = this;
        }
    }
}