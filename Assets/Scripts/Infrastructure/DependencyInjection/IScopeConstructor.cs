using System;

namespace Infrastructure.DependencyInjection
{
    public interface IScopeConstructor
    {
        Scope Construct(Scope parentScope, Action<IRuleResolver> initialize);

        Scope ConstructPartial(Scope mainScope, Action<IRuleResolver> initialize);
    }
}