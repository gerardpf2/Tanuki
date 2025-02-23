using System;
using Infrastructure.DependencyInjection.Resolvers;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class ScopeResolver : IScopeResolver
    {
        private readonly IResolverContainer _resolverContainer;
        private readonly IScopeResolver _parentScopeResolver;

        public ScopeResolver([NotNull] IResolverContainer resolverContainer, IScopeResolver parentScopeResolver)
        {
            _resolverContainer = resolverContainer;
            _parentScopeResolver = parentScopeResolver;
        }

        public T Resolve<T>()
        {
            if (TryResolve(out T result))
            {
                return result;
            }

            throw new InvalidOperationException(); // TODO
        }

        public bool TryResolve<T>(out T result)
        {
            if (_resolverContainer.TryGet(out IResolver<T> resolver))
            {
                result = resolver.Resolve(this);

                if (result == null)
                {
                    throw new InvalidOperationException(); // TODO
                }

                return true;
            }

            if (_parentScopeResolver != null)
            {
                return _parentScopeResolver.TryResolve(out result);
            }

            result = default;

            return false;
        }
    }
}