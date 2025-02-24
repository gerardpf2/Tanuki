using System;
using System.Collections.Generic;

namespace Infrastructure.DependencyInjection
{
    public interface IScopeBuildingContext
    {
        void SetAddRules(Action<IRuleContainer> addRules);

        void SetGetPartialScopeComposers(Func<IEnumerable<IScopeComposer>> getPartialScopeComposers);

        void SetGetChildScopeComposers(Func<IEnumerable<IScopeComposer>> getChildScopeComposers);

        void SetInitialize(Action<IRuleResolver> initialize);
    }
}