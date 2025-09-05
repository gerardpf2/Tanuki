using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Infrastructure.ScreenLoading.Composition
{
    public class ScreenLoadingComposer : ScopeComposer
    {
        [NotNull] private readonly IScreenDefinitionGetter _screenDefinitionGetter;
        [NotNull] private readonly IScreenPlacement _rootScreenPlacement;

        public ScreenLoadingComposer(
            [NotNull] IScreenDefinitionGetter screenDefinitionGetter,
            [NotNull] IScreenPlacement rootScreenPlacement)
        {
            ArgumentNullException.ThrowIfNull(screenDefinitionGetter);
            ArgumentNullException.ThrowIfNull(rootScreenPlacement);

            _screenDefinitionGetter = screenDefinitionGetter;
            _rootScreenPlacement = rootScreenPlacement;
        }

        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(ruleFactory.GetInstance(_screenDefinitionGetter));

            ruleAdder.Add(ruleFactory.GetInstance(_rootScreenPlacement));

            ruleAdder.Add(ruleFactory.GetSingleton(_ => new ScreenPlacementContainer()));
            ruleAdder.Add(ruleFactory.GetTo<IScreenPlacementAdder, ScreenPlacementContainer>());
            ruleAdder.Add(ruleFactory.GetTo<IScreenPlacementGetter, ScreenPlacementContainer>());

            ruleAdder.Add(
                ruleFactory.GetSingleton<IScreenLoader>(r =>
                    new ScreenLoader(
                        r.Resolve<IScreenDefinitionGetter>(),
                        r.Resolve<IScreenPlacementGetter>()
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