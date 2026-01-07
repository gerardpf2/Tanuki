using Game.Gameplay.View.PauseMenu.UseCases;
using Game.Gameplay.View.UseCases;
using Infrastructure.DependencyInjection;
using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.PauseMenu.Composition
{
    public class PauseMenuComposer : ScopeComposer
    {
        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetSingleton<IGoToMainMenuUseCase>(r =>
                    new GoToMainMenuUseCase(
                        r.Resolve<IUnloadPauseMenuUseCase>(),
                        r.Resolve<IUnloadGameplayUseCase>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<ILoadPauseMenuUseCase>(r =>
                    new LoadPauseMenuUseCase(
                        r.Resolve<IScreenLoader>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IResumeGameplayUseCase>(r =>
                    new ResumeGameplayUseCase(
                        r.Resolve<IUnloadPauseMenuUseCase>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IUnloadPauseMenuUseCase>(r =>
                    new UnloadPauseMenuUseCase(
                        r.Resolve<IScreenLoader>()
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
                ruleFactory.GetInject<PauseMenuViewModel>((r, s) =>
                    s.Inject(
                        r.Resolve<IResumeGameplayUseCase>(),
                        r.Resolve<IGoToMainMenuUseCase>()
                    )
                )
            );
        }
    }
}