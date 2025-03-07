using System;

namespace Infrastructure.DependencyInjection
{
    public interface IScopeConstructor
    {
        PartialScope ConstructPartial(Scope mainScope, Action<IRuleResolver> initialize);

        Scope Construct(Scope parentScope, Action<IRuleResolver> initialize);
    }
}