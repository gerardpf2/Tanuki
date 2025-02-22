using System;
using Infrastructure.DependencyInjection.ServiceResolvers;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class CompositionScope : ICompositionScope
    {
        private readonly IServiceResolverContainer _serviceResolverContainer;
        private readonly ICompositionScope _parentCompositionScope;

        public CompositionScope(
            [NotNull] IServiceResolverContainer serviceResolverContainer,
            ICompositionScope parentCompositionScope)
        {
            _serviceResolverContainer = serviceResolverContainer;
            _parentCompositionScope = parentCompositionScope;
        }

        public T Resolve<T>()
        {
            if (TryResolve(out T service))
            {
                return service;
            }

            throw new InvalidOperationException(); // TODO
        }

        public bool TryResolve<T>(out T service)
        {
            if (_serviceResolverContainer.TryGet(out IServiceResolver<T> serviceResolver))
            {
                service = serviceResolver.Resolve(this);

                if (service == null)
                {
                    throw new InvalidOperationException(); // TODO
                }

                return true;
            }

            if (_parentCompositionScope != null)
            {
                return _parentCompositionScope.TryResolve(out service);
            }

            service = default;

            return false;
        }
    }
}