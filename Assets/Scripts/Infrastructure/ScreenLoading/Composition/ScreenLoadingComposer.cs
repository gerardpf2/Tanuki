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

        protected override void AddPrivateRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            base.AddPrivateRules(ruleAdder, ruleFactory);

            ruleAdder.Add(ruleFactory.GetInstance(_screenDefinitionGetter));

            ruleAdder.Add(ruleFactory.GetInstance(_rootScreenPlacement));

            ruleAdder.Add(ruleFactory.GetSingleton(_ => new ScreenPlacementContainer()));
            ruleAdder.Add(ruleFactory.GetTo<IScreenPlacementAdder, ScreenPlacementContainer>());
            ruleAdder.Add(ruleFactory.GetTo<IScreenPlacementGetter, ScreenPlacementContainer>());
        }

        protected override void AddPublicRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            base.AddPublicRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetSingleton<IScreenLoader>(r =>
                    new ScreenLoader(
                        r.Resolve<IScreenDefinitionGetter>(),
                        r.Resolve<IScreenPlacementGetter>()
                    )
                )
            );
        }

        protected override void AddGlobalRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            base.AddGlobalRules(ruleAdder, ruleFactory);

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

            ruleResolver.Resolve<IScreenPlacementAdder>().Add(ruleResolver.Resolve<IScreenPlacement>());
        }
    }
}