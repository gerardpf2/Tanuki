using Game.Gameplay.Goals.Parsing;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Goals.Composition
{
    public class GoalsComposer : ScopeComposer
    {
        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(ruleFactory.GetSingleton<IGoalSerializedDataConverter>(_ => new GoalSerializedDataConverter()));

            ruleAdder.Add(
                ruleFactory.GetSingleton<IGoalsSerializedDataConverter>(r =>
                    new GoalsSerializedDataConverter(
                        r.Resolve<IGoalSerializedDataConverter>()
                    )
                )
            );

            ruleAdder.Add(ruleFactory.GetSingleton<IGoals>(_ => new Goals()));
        }
    }
}