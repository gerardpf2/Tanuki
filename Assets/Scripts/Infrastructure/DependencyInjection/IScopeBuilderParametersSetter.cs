using System;
using System.Collections.Generic;

namespace Infrastructure.DependencyInjection
{
    public interface IScopeBuilderParametersSetter
    {
        void SetAddResolvers(Action<IResolverContainer> addResolvers);

        void SetInitialize(Action<IScopeResolver> initialize);

        void SetAddChildScopeComposers(Action<ICollection<IScopeComposer>> addChildScopeComposers);
    }
}