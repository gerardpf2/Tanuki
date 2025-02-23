using System.Collections.Generic;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class ScopeComposer : IScopeComposer
    {
        public void Compose([NotNull] IScopeBuilderParametersSetter scopeBuilderParametersSetter)
        {
            scopeBuilderParametersSetter.SetAddResolvers(AddResolvers);
            scopeBuilderParametersSetter.SetInitialize(Initialize);
            scopeBuilderParametersSetter.SetAddChildScopeComposers(AddChildScopeComposers);
        }

        protected virtual void AddResolvers(IResolverContainer resolverContainer) { }

        protected virtual void Initialize(IScopeResolver scopeResolver) { }

        protected virtual void AddChildScopeComposers(ICollection<IScopeComposer> childScopeComposers) { }
    }
}