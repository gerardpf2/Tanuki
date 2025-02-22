using System;
using System.Collections.Generic;
using Infrastructure.DependencyInjection.ServiceResolvers;

namespace Infrastructure.DependencyInjection
{
    public class ServiceResolverContainer : IServiceResolverContainer
    {
        private readonly IDictionary<Type, IServiceResolver<object>> _serviceResolvers = new Dictionary<Type, IServiceResolver<object>>();

        public void Add<T>(IServiceResolver<T> serviceResolver)
        {
            if (serviceResolver is IServiceResolver<object> serviceResolverO && _serviceResolvers.TryAdd(typeof(T), serviceResolverO))
            {
                return;
            }

            throw new InvalidOperationException(); // TODO
        }

        public bool TryGet<T>(out IServiceResolver<T> serviceResolver)
        {
            if (_serviceResolvers.TryGetValue(typeof(T), out IServiceResolver<object> serviceResolverO) && serviceResolverO is IServiceResolver<T> serviceResolverT)
            {
                serviceResolver = serviceResolverT;

                return true;
            }

            serviceResolver = null;

            return false;
        }
    }
}