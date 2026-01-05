using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Infrastructure.ScreenLoading.Composition
{
    public class ScreenLoadingComposer : ScopeComposer
    {
        [NotNull] private readonly IScreenGetter _screenGetter;
        [NotNull] private readonly IScreenPlacement _rootScreenPlacement;

        public ScreenLoadingComposer(
            [NotNull] IScreenGetter screenGetter,
            [NotNull] IScreenPlacement rootScreenPlacement)
        {
            ArgumentNullException.ThrowIfNull(screenGetter);
            ArgumentNullException.ThrowIfNull(rootScreenPlacement);

            _screenGetter = screenGetter;
            _rootScreenPlacement = rootScreenPlacement;
        }

        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(ruleFactory.GetInstance(_screenGetter));

            ruleAdder.Add(ruleFactory.GetInstance(_rootScreenPlacement));

            ruleAdder.Add(ruleFactory.GetSingleton(_ => new ScreenPlacementContainer()));
            ruleAdder.Add(ruleFactory.GetTo<IScreenPlacementAdder, ScreenPlacementContainer>());
            ruleAdder.Add(ruleFactory.GetTo<IScreenPlacementGetter, ScreenPlacementContainer>());

            ruleAdder.Add(ruleFactory.GetSingleton<IScreenStack>(_ => new ScreenStack()));

            ruleAdder.Add(
                ruleFactory.GetSingleton<IScreenLoader>(r =>
                    new ScreenLoader(
                        r.Resolve<IScreenGetter>(),
                        r.Resolve<IScreenPlacementGetter>(),
                        r.Resolve<IScreenStack>()
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
                ruleFactory.GetInject<ScreenPlacement>((r, s) =>
                    s.Inject(
                        r.Resolve<IScreenPlacementAdder>()
                    )
                )
            );
        }

        protected override void Initialize([NotNull] IRuleResolver ruleResolver)
        {
            ArgumentNullException.ThrowIfNull(ruleResolver);

            base.Initialize(ruleResolver);

            ruleResolver.Resolve<IScreenPlacementAdder>().Add(ruleResolver.Resolve<IScreenPlacement>());
        }
    }
}