using Game.Gameplay.Bag;
using JetBrains.Annotations;

namespace Game.Gameplay.Parsing
{
    public interface IGameplayParser
    {
        string Serialize(IBag bag);

        void Deserialize(string value, [NotNull] out IBag bag);
    }
}