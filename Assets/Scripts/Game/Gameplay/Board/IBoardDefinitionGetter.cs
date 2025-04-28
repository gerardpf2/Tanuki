using JetBrains.Annotations;

namespace Game.Gameplay.Board
{
    public interface IBoardDefinitionGetter
    {
        [NotNull]
        IBoardDefinition Get(string id);
    }
}