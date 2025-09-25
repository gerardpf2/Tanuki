using JetBrains.Annotations;

namespace Game.Gameplay.Goals.Parsing
{
    public interface IGoalsParser
    {
        string Serialize(IGoals goals);

        [NotNull]
        IGoals Deserialize(string value);
    }
}