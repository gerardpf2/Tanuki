using JetBrains.Annotations;

namespace Game.Gameplay.Goals.Parsing
{
    public interface IGoalSerializedDataConverter
    {
        [NotNull]
        IGoal To(GoalSerializedData goalSerializedData);

        [NotNull]
        GoalSerializedData From(IGoal goal);
    }
}