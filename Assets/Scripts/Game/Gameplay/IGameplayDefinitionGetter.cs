using JetBrains.Annotations;

namespace Game.Gameplay
{
    public interface IGameplayDefinitionGetter
    {
        [NotNull]
        IGameplayDefinition Get(string id);
    }
}