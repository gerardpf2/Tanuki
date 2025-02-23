using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class ScopeBuilder : IScopeBuilderParametersSetter, IScopeBuilder
    {
        private Action<IResolverContainer> _addResolvers;
        private Action<IScopeResolver> _initialize;
        private Action<ICollection<IScopeComposer>> _addChildScopeComposers;

        public void SetAddResolvers(Action<IResolverContainer> addResolvers)
        {
            _addResolvers = addResolvers;
        }

        public void SetInitialize(Action<IScopeResolver> initialize)
        {
            _initialize = initialize;
        }

        public void SetAddChildScopeComposers(Action<ICollection<IScopeComposer>> addChildScopeComposers)
        {
            _addChildScopeComposers = addChildScopeComposers;
        }

        public Scope Build([NotNull] IScopeComposer scopeComposer, IScopeResolver parentScopeResolver)
        {
            Clear();

            scopeComposer.Compose(this);

            IResolverContainer resolverContainer = new ResolverContainer();
            IScopeResolver scopeResolver = new ScopeResolver(resolverContainer, parentScopeResolver);
            ICollection<IScopeComposer> childScopeComposers = new List<IScopeComposer>();
            Scope scope = new(scopeComposer, resolverContainer, scopeResolver);

            _addResolvers?.Invoke(resolverContainer);
            _initialize?.Invoke(scopeResolver);
            _addChildScopeComposers?.Invoke(childScopeComposers);

            foreach (IScopeComposer childScopeComposer in childScopeComposers)
            {
                Scope childScope = Build(childScopeComposer, scopeResolver);
                scope.AddChild(childScope);
            }

            return scope;
        }

        private void Clear()
        {
            _addResolvers = null;
            _initialize = null;
            _addChildScopeComposers = null;
        }
    }
}