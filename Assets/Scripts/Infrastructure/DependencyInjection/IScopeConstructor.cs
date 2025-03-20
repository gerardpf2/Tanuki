using System;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public interface IScopeConstructor
    {
        [NotNull]
        PartialScope ConstructPartial(Scope mainScope, Action<IRuleResolver> initialize);

        [NotNull]
        Scope Construct(Scope parentScope, Action<IRuleResolver> initialize);
    }
}