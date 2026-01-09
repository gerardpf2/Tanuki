using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Goals.Parsing
{
    public class GoalSerializedDataConverter : IGoalSerializedDataConverter
    {
        public IGoal To([NotNull] GoalSerializedData goalSerializedData)
        {
            ArgumentNullException.ThrowIfNull(goalSerializedData);

            return
                new Goal(
                    goalSerializedData.PieceType,
                    goalSerializedData.InitialAmount,
                    goalSerializedData.CurrentAmount
                );
        }

        public GoalSerializedData From([NotNull] IGoal goal)
        {
            ArgumentNullException.ThrowIfNull(goal);

            return
                new GoalSerializedData
                {
                    PieceType = goal.PieceType,
                    InitialAmount = goal.InitialAmount,
                    CurrentAmount = goal.CurrentAmount
                };
        }
    }
}