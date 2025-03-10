using Infrastructure.DependencyInjection;

namespace Game.Root.UseCases
{
    public interface IBuildAndInitializeRootScopeUseCase
    {
        Scope Resolve();
    }
}