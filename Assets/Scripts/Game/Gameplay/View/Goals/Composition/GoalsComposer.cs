using Game.Gameplay.Goals;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Goals.Composition
{
    public class GoalsComposer : ScopeComposer
    {
        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(ruleFactory.GetSingleton<IGoals>(_ => new Gameplay.Goals.Goals()), GoalsComposerKeys.Goals.View);

            ruleAdder.Add(
                ruleFactory.GetSingleton<IGoalsView>(r =>
                    new GoalsView(
                        r.Resolve<IGoals>(),
                        r.Resolve<IGoals>(GoalsComposerKeys.Goals.View)
                    )
                )
            );
        }

        protected override void AddSharedRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddSharedRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetInject<GoalsViewModel>((r, vm) =>
                    vm.Inject(
                        r.Resolve<IGoalsView>()
                    )
                )
            );
        }
    }
}