using System.Collections.Generic;
using System.Linq;
using Infrastructure.Configuring;
using Infrastructure.Configuring.Composition;
using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Composition;
using Infrastructure.Logging.Composition;
using Infrastructure.ModelViewViewModel.Composition;
using Infrastructure.ScreenLoading;
using Infrastructure.ScreenLoading.Composition;
using Infrastructure.System;
using Infrastructure.System.Exceptions;
using Infrastructure.Tweening.Composition;
using Infrastructure.Unity;
using Infrastructure.Unity.Composition;
using JetBrains.Annotations;

namespace Game.Root.Composition
{
    public class RootComposer : ScopeComposer
    {
        [NotNull] private readonly IScreenDefinitionGetter _screenDefinitionGetter;
        [NotNull] private readonly IScreenPlacement _rootScreenPlacement;
        [NotNull] private readonly IConfigValueGetter _configValueGetter;
        [NotNull] private readonly ICoroutineRunner _coroutineRunner;
        [NotNull] private readonly IConverter _converter;

        public RootComposer(
            [NotNull] IScreenDefinitionGetter screenDefinitionGetter,
            [NotNull] IScreenPlacement rootScreenPlacement,
            [NotNull] IConfigValueGetter configValueGetter,
            [NotNull] ICoroutineRunner coroutineRunner,
            [NotNull] IConverter converter)
        {
            ArgumentNullException.ThrowIfNull(screenDefinitionGetter);
            ArgumentNullException.ThrowIfNull(rootScreenPlacement);
            ArgumentNullException.ThrowIfNull(configValueGetter);
            ArgumentNullException.ThrowIfNull(coroutineRunner);
            ArgumentNullException.ThrowIfNull(converter);

            _screenDefinitionGetter = screenDefinitionGetter;
            _rootScreenPlacement = rootScreenPlacement;
            _configValueGetter = configValueGetter;
            _coroutineRunner = coroutineRunner;
            _converter = converter;
        }

        protected override IEnumerable<IScopeComposer> GetPartialScopeComposers()
        {
            return base
                .GetPartialScopeComposers()
                .Append(new LoggingComposer())
                .Append(new ScreenLoadingComposer(_screenDefinitionGetter, _rootScreenPlacement))
                .Append(new ConfiguringComposer(_configValueGetter))
                .Append(new TweeningComposer())
                .Append(new UnityComposer(_coroutineRunner))
                .Append(new SystemComposer(_converter));
        }

        protected override IEnumerable<IScopeComposer> GetChildScopeComposers()
        {
            return base.GetChildScopeComposers().Append(new ModelViewViewModelComposer());
        }
    }
}