using System;
using System.Collections.Generic;

namespace Infrastructure.DependencyInjection
{
    public interface IScopeBuildingContext
    {
        void SetAddRules(Action<IRuleContainer> addRules);

        void SetInitialize(Action<IRuleResolver> initialize);

        void SetGetChildScopeComposers(Func<IEnumerable<IScopeComposer>> getChildScopeComposers);
    }
}