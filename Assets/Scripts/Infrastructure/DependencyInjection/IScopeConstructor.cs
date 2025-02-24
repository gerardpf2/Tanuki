using System;

namespace Infrastructure.DependencyInjection
{
    public interface IScopeConstructor
    {
        Scope Construct(IScopeComposer scopeComposer, Scope parentScope, Action<IRuleResolver> initialize);

        Scope Construct(
            IScopeComposer scopeComposer,
            IRuleContainer ruleContainer,
            IRuleResolver ruleResolver,
            Action<IRuleResolver> initialize
        );
    }
}