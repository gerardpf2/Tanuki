using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public interface IRuleResolver
    {
        [NotNull]
        T Resolve<T>(object key = null);

        bool TryResolve<T>(out T result, object key = null);
    }
}