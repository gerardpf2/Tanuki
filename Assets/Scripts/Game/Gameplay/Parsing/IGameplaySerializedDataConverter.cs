using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Goals;
using Game.Gameplay.Moves;
using JetBrains.Annotations;

namespace Game.Gameplay.Parsing
{
    public interface IGameplaySerializedDataConverter
    {
        void To(GameplaySerializedData gameplaySerializedData, IBoard board, IGoals goals, IMoves moves, IBag bag);

        [NotNull]
        GameplaySerializedData From(IBoard board, IGoals goals, IMoves moves, IBag bag);
    }
}