using JetBrains.Annotations;

namespace Game.Gameplay.Goals.Parsing
{
    public interface IGoalsSerializedDataConverter
    {
        [NotNull]
        IGoals To(GoalsSerializedData goalsSerializedData);

        [NotNull]
        GoalsSerializedData From(IGoals goals);
    }
}