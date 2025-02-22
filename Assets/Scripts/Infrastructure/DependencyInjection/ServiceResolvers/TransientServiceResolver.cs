using System;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.ServiceResolvers
{
    public class TransientServiceResolver<T> : IServiceResolver<T>
    {
        private readonly Func<ICompositionScope, T> _ctor;

        public TransientServiceResolver([NotNull] Func<ICompositionScope, T> ctor)
        {
            _ctor = ctor;
        }

        public T Resolve(ICompositionScope compositionScope)
        {
            return _ctor(compositionScope);
        }
    }
}