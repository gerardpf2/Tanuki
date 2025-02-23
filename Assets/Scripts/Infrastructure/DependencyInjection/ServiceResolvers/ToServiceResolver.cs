using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.ServiceResolvers
{
    public class ToServiceResolver<TI, TO> : IServiceResolver<TI> where TO : TI
    {
        public TI Resolve([NotNull] ICompositionScope compositionScope)
        {
            return compositionScope.Resolve<TO>();
        }
    }
}