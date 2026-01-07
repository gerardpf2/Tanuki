using Game.Gameplay.View.EventResolvers;
using Game.Gameplay.View.PauseMenu.UseCases;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Header.Composition
{
    public class HeaderComposer : ScopeComposer
    {
        protected override void AddSharedRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddSharedRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetInject<HeaderViewModel>((r, s) =>
                    s.Inject(
                        r.Resolve<IEventsResolver>(),
                        r.Resolve<ILoadPauseMenuUseCase>()
                    )
                )
            );
        }
    }
}