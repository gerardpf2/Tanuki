using System.Collections.Generic;

namespace Infrastructure.DependencyInjection
{
    public class Scope
    {
        public IReadOnlyCollection<Scope> ChildScopes => _childScopes;

        public readonly IScopeComposer ScopeComposer;
        public readonly IResolverContainer ResolverContainer;
        public readonly IScopeResolver ScopeResolver;

        private readonly List<Scope> _childScopes = new();

        public Scope(IScopeComposer scopeComposer, IResolverContainer resolverContainer, IScopeResolver scopeResolver)
        {
            ScopeComposer = scopeComposer;
            ResolverContainer = resolverContainer;
            ScopeResolver = scopeResolver;
        }

        public void AddChild(Scope childScope)
        {
            _childScopes.Add(childScope);
        }
    }
}