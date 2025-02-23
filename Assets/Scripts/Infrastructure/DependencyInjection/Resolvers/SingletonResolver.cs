using System;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.Resolvers
{
    public class SingletonResolver<T> : IResolver<T>
    {
        private readonly Func<IScopeResolver, T> _ctor;

        private T _instance;

        public SingletonResolver([NotNull] Func<IScopeResolver, T> ctor)
        {
            _ctor = ctor;
        }

        public T Resolve(IScopeResolver scopeResolver)
        {
            return _instance ??= _ctor(scopeResolver);
        }
    }
}