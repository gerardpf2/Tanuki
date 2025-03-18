using System.Collections.Generic;
using System.Linq;
using Infrastructure.DependencyInjection;
using Infrastructure.Logging.Composition;
using Infrastructure.ModelViewViewModel.Composition;
using Infrastructure.ScreenLoading;
using Infrastructure.ScreenLoading.Composition;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Root.Composition
{
    public class RootComposer : ScopeComposer
    {
        [NotNull] private readonly IScreenDefinitionGetter _screenDefinitionGetter;
        [NotNull] private readonly IScreenPlacement _rootScreenPlacement;

        public RootComposer(
            [NotNull] IScreenDefinitionGetter screenDefinitionGetter,
            [NotNull] IScreenPlacement rootScreenPlacement)
        {
            ArgumentNullException.ThrowIfNull(screenDefinitionGetter);
            ArgumentNullException.ThrowIfNull(rootScreenPlacement);

            _screenDefinitionGetter = screenDefinitionGetter;
            _rootScreenPlacement = rootScreenPlacement;
        }

        protected override IEnumerable<IScopeComposer> GetPartialScopeComposers()
        {
            InvalidOperationException.ThrowIfNull(_screenDefinitionGetter);
            InvalidOperationException.ThrowIfNull(_rootScreenPlacement);

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