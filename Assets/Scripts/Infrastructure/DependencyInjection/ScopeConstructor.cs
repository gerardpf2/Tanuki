using System;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class ScopeConstructor : IScopeConstructor
    {
        public PartialScope ConstructPartial([NotNull] Scope mainScope, Action<IRuleResolver> initialize)
        {
            RuleContainer privateRuleContainer = new();
            IRuleResolver ruleResolver = new RuleResolver(
                privateRuleContainer,
                mainScope.PublicRuleGetter,
                mainScope.RuleResolver
            );
            PartialScope partialScope = new(
                mainScope,
                privateRuleContainer,
                mainScope.PublicRuleAdder,
                mainScope.PublicRuleGetter,
                ruleResolver,
                initialize
            );

            mainScope.AddPartial(partialScope);

            return partialScope;
        }

        public Scope Construct(Scope parentScope, Action<IRuleResolver> initialize)
        {
            RuleContainer privateRuleContainer = new();
            RuleContainer publicRuleContainer = new();
            IRuleResolver ruleResolver = new RuleResolver(
                privateRuleContainer,
                publicRuleContainer,
                parentScope?.RuleResolver
            );
            Scope childScope = new(
                privateRuleContainer,
                publicRuleContainer,
                publicRuleContainer,
                ruleResolver,
                initialize
            );

            parentScope?.AddChild(childScope);

            return childScope;
        }
    }
}