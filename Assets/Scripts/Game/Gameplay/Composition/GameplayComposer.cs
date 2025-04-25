using Game.Gameplay.UseCases;
using Infrastructure.DependencyInjection;
using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Composition
{
    public class GameplayComposer : ScopeComposer
    {
        protected override void AddSharedRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddSharedRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetSingleton<ILoadGameplay>(r =>
                    new LoadGameplay(
                        r.Resolve<IScreenLoader>()
                    )
                )
            );
        }
    }
}