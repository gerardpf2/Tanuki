using System.Collections.Generic;
using System.Linq;
using Infrastructure.DependencyInjection;
using Infrastructure.Logging.Composition;
using Infrastructure.ModelViewViewModel.Composition;
using Infrastructure.ScreenLoading;
using Infrastructure.ScreenLoading.Composition;
using JetBrains.Annotations;

namespace Game.Root.Composition
{
    public class RootComposer : ScopeComposer
    {
        private readonly IScreenDefinitionGetter _screenDefinitionGetter;
        private readonly IScreenPlacement _rootScreenPlacement;

        public RootComposer(
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
        }

        protected override IEnumerable<IScopeComposer> GetPartialScopeComposers([NotNull] IRuleResolver ruleResolver)
        {
            return
                base
                    .GetPartialScopeComposers(ruleResolver)
                    .Append(new LoggingComposer())
                    .Append(
                        new ScreenLoadingComposer(
                            ruleResolver.Resolve<IScreenDefinitionGetter>(),
                            ruleResolver.Resolve<IScreenPlacement>()
                        )
                    );
        }

        protected override IEnumerable<IScopeComposer> GetChildScopeComposers()
        {
            return base.GetChildScopeComposers().Append(new ModelViewViewModelComposer());
        }
    }
}