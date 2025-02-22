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

        public T Resolve<T>() where T : class
        {
            if (TryResolve(out T service))
            {
                return service;
            }

            throw new InvalidOperationException(); // TODO
        }

        public bool TryResolve<T>(out T service) where T : class
        {
            if (_serviceResolverContainer.TryGet(out IServiceResolver<T> serviceResolver))
            {
                service = serviceResolver.Resolve(this);

                if (service == null)
                {
                    throw new InvalidOperationException(); // TODO
                }
            }
            else if (_parentCompositionScope != null)
            {
                return _parentCompositionScope.TryResolve(out service);
            }
            else
            {
                service = null;
            }

            return service != null;
        }
    }
}