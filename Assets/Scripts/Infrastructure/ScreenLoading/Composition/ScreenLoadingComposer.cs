using Infrastructure.DependencyInjection;
using JetBrains.Annotations;

namespace Infrastructure.ScreenLoading.Composition
{
    public class ScreenLoadingComposer : ScopeComposer
    {
        private readonly IScreenDefinitionGetter _screenDefinitionGetter;
        private readonly IScreenPlacement _rootScreenPlacement;

        public ScreenLoadingComposer(
            [NotNull] IScreenDefinitionGetter screenDefinitionGetter,
            [NotNull] IScreenPlacement rootScreenPlacement)
        {
            _screenDefinitionGetter = screenDefinitionGetter;
            _rootScreenPlacement = rootScreenPlacement;
        }

        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetSingleton<IScreenLoader>(r =>
                    new ScreenLoader(
                        _screenDefinitionGetter,
                        r.Resolve<IScreenPlacementGetter>()
                    )
                )
            );

            ruleAdder.Add(ruleFactory.GetSingleton(_ => new ScreenPlacementContainer()));
            ruleAdder.Add(ruleFactory.GetTo<IScreenPlacementAdder, ScreenPlacementContainer>());
            ruleAdder.Add(ruleFactory.GetTo<IScreenPlacementGetter, ScreenPlacementContainer>());
        }

        protected override void AddSharedRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            base.AddSharedRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetInject<ScreenPlacement>((r, sp) =>
                    sp.Inject(
                        r.Resolve<IScreenPlacementAdder>()
                    )
                )
            );
        }

        protected override void Initialize([NotNull] IRuleResolver ruleResolver)
        {
            base.Initialize(ruleResolver);

            ruleResolver.Resolve<IScreenPlacementAdder>().Add(_rootScreenPlacement);
        }
    }
}