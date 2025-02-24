using System;

namespace Infrastructure.DependencyInjection
{
    public interface IScopeConstructor
    {
        Scope ConstructPartialOf(Scope scope, IScopeComposer scopeComposer, Action<IRuleResolver> initialize);

        Scope ConstructChildOf(Scope scope, IScopeComposer scopeComposer, Action<IRuleResolver> initialize);
    }
}