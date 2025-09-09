using Infrastructure.DependencyInjection;
using JetBrains.Annotations;

namespace Game
{
    public interface IGameScopeComposerBuilder
    {
        [NotNull]
        IScopeComposer Build();
    }
}