using Infrastructure.DependencyInjection;

namespace Root.UseCases
{
    public interface IBuildAndInitializeRootScopeUseCase
    {
        Scope Resolve();
    }
}