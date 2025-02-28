using System;

namespace Infrastructure.DependencyInjection
{
    public interface IScopeConstructor
    {
        Scope ConstructPartialOf(Scope scope, Action<IRuleResolver> initialize);

        Scope ConstructChildOf(Scope scope, Action<IRuleResolver> initialize);
    }
}