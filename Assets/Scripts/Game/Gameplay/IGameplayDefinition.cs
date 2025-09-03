using Game.Gameplay.Board;
using JetBrains.Annotations;

namespace Game.Gameplay
{
    public interface IGameplayDefinition
    {
        string Id { get; }

        [NotNull]
        IBoardDefinition BoardDefinition { get; }
    }
}