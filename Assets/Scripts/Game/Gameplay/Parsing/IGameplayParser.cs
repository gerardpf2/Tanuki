using Game.Gameplay.Bag;
using Game.Gameplay.Goals;
using JetBrains.Annotations;

namespace Game.Gameplay.Parsing
{
    public interface IGameplayParser
    {
        string Serialize(IGoals goals, IBag bag);

        void Deserialize(string value, [NotNull] out IGoals goals, [NotNull] out IBag bag);
    }
}