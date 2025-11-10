using JetBrains.Annotations;

namespace Game.Gameplay.Goals.Parsing
{
    public interface IGoalsSerializedDataConverter
    {
        void To(GoalsSerializedData goalsSerializedData, IGoals goals);

        [NotNull]
        GoalsSerializedData From(IGoals goals);
    }
}