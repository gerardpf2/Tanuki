using System.Linq;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Goals.Parsing
{
    public class GoalsSerializedDataConverter : IGoalsSerializedDataConverter
    {
        [NotNull] private readonly IGoalSerializedDataConverter _goalSerializedDataConverter;

        public GoalsSerializedDataConverter([NotNull] IGoalSerializedDataConverter goalSerializedDataConverter)
        {
            ArgumentNullException.ThrowIfNull(goalSerializedDataConverter);

            _goalSerializedDataConverter = goalSerializedDataConverter;
        }

        public IGoals To([NotNull] GoalsSerializedData goalsSerializedData)
        {
            ArgumentNullException.ThrowIfNull(goalsSerializedData);

            return new Goals(goalsSerializedData.GoalSerializedData.Select(_goalSerializedDataConverter.To));
        }

        public GoalsSerializedData From([NotNull] IGoals goals)
        {
            ArgumentNullException.ThrowIfNull(goals);

            return
                new GoalsSerializedData
                {
                    GoalSerializedData = goals.Targets.Select(_goalSerializedDataConverter.From).ToList()
                };
        }
    }
}