using System;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.ServiceResolvers
{
    public class SingletonServiceResolver<T> : IServiceResolver<T>
    {
        private readonly Func<ICompositionScope, T> _ctor;

        private T _instance;

        public SingletonServiceResolver([NotNull] Func<ICompositionScope, T> ctor)
        {
            _ctor = ctor;
        }

        public T Resolve(ICompositionScope compositionScope)
        {
            return _instance ??= _ctor(compositionScope);
        }
    }
}