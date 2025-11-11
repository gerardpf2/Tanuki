using Game.Gameplay.Bag;
using Game.Gameplay.Goals;
using Game.Gameplay.Moves;
using JetBrains.Annotations;

namespace Game.Gameplay.Parsing
{
    public interface IGameplayParser
    {
        string Serialize(IGoals goals, IMoves moves, IBag bag);

        void Deserialize(string value, [NotNull] out IGoals goals, [NotNull] out IMoves moves, [NotNull] out IBag bag);
    }
}