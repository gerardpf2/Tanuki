using System.Collections.Generic;
using System.Linq;
using Infrastructure.Configuring;
using Infrastructure.Configuring.Composition;
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
        [NotNull] private readonly IConfigValueGetter _configValueGetter;

        public RootComposer(
            [NotNull] IScreenDefinitionGetter screenDefinitionGetter,
            [NotNull] IScreenPlacement rootScreenPlacement,
            [NotNull] IConfigValueGetter configValueGetter)
        {
            ArgumentNullException.ThrowIfNull(screenDefinitionGetter);
            ArgumentNullException.ThrowIfNull(rootScreenPlacement);
            ArgumentNullException.ThrowIfNull(configValueGetter);

            _screenDefinitionGetter = screenDefinitionGetter;
            _rootScreenPlacement = rootScreenPlacement;
            _configValueGetter = configValueGetter;
        }

        protected override IEnumerable<IScopeComposer> GetPartialScopeComposers()
        {
            return base
                .GetPartialScopeComposers()
                .Append(new LoggingComposer())
                .Append(new ScreenLoadingComposer(_screenDefinitionGetter, _rootScreenPlacement))
                .Append(new ConfiguringComposer(_configValueGetter));
        }

        protected override IEnumerable<IScopeComposer> GetChildScopeComposers()
        {
            return base.GetChildScopeComposers().Append(new ModelViewViewModelComposer());
        }
    }
}