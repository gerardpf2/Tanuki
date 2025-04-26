using JetBrains.Annotations;

namespace Game.Gameplay.Model.Board
{
    public interface IBoardDefinitionGetter
    {
        [NotNull]
        IBoardDefinition Get(string id);
    }
}