using System;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.Resolvers
{
    public class TransientResolver<T> : IResolver<T>
    {
        private readonly Func<IScopeResolver, T> _ctor;

        public TransientResolver([NotNull] Func<IScopeResolver, T> ctor)
        {
            _ctor = ctor;
        }

        public T Resolve(IScopeResolver scopeResolver)
        {
            return _ctor(scopeResolver);
        }
    }
}