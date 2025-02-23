using System;
using System.Collections.Generic;
using Infrastructure.DependencyInjection.Resolvers;

namespace Infrastructure.DependencyInjection
{
    public class ResolverContainer : IResolverContainer
    {
        private readonly IDictionary<Type, IResolver<object>> _resolvers = new Dictionary<Type, IResolver<object>>();

        public void Add<T>(IResolver<T> resolver)
        {
            if (resolver is IResolver<object> resolverO && _resolvers.TryAdd(typeof(T), resolverO))
            {
                return;
            }

            throw new InvalidOperationException(); // TODO
        }

        public bool TryGet<T>(out IResolver<T> resolver)
        {
            if (_resolvers.TryGetValue(typeof(T), out IResolver<object> resolverO) && resolverO is IResolver<T> resolverT)
            {
                resolver = resolverT;

                return true;
            }

            resolver = null;

            return false;
        }
    }
}