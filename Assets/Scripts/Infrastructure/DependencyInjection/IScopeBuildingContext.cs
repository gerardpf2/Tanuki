using System;
using System.Collections.Generic;

namespace Infrastructure.DependencyInjection
{
    public interface IScopeBuildingContext
    {
        void SetAddRules(Action<IRuleContainer> addRules);

        void SetInitialize(Action<IRuleResolver> initialize);

        void SetAddChildScopeComposers(Action<ICollection<IScopeComposer>> addChildScopeComposers);
    }
}