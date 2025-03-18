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

        protected override IEnumerable<IScopeComposer> GetPartialScopeComposers()
        {
            return base
                .GetPartialScopeComposers()
                .Append(new LoggingComposer())
                .Append(new ScreenLoadingComposer(_screenDefinitionGetter, _rootScreenPlacement));
        }

        protected override IEnumerable<IScopeComposer> GetChildScopeComposers()
        {
            return base.GetChildScopeComposers().Append(new ModelViewViewModelComposer());
        }
    }
}