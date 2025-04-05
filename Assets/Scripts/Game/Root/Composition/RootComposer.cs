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
using Infrastructure.Tweening.Composition;
using Infrastructure.Unity;
using JetBrains.Annotations;

namespace Game.Root.Composition
{
    public class RootComposer : ScopeComposer
    {
        [NotNull] private readonly IScreenDefinitionGetter _screenDefinitionGetter;
        [NotNull] private readonly IScreenPlacement _rootScreenPlacement;
        [NotNull] private readonly IConfigValueGetter _configValueGetter;
        [NotNull] private readonly ICoroutineRunner _coroutineRunner;

        public RootComposer(
            [NotNull] IScreenDefinitionGetter screenDefinitionGetter,
            [NotNull] IScreenPlacement rootScreenPlacement,
            [NotNull] IConfigValueGetter configValueGetter,
            [NotNull] ICoroutineRunner coroutineRunner)
        {
            ArgumentNullException.ThrowIfNull(screenDefinitionGetter);
            ArgumentNullException.ThrowIfNull(rootScreenPlacement);
            ArgumentNullException.ThrowIfNull(configValueGetter);
            ArgumentNullException.ThrowIfNull(coroutineRunner);

            _screenDefinitionGetter = screenDefinitionGetter;
            _rootScreenPlacement = rootScreenPlacement;
            _configValueGetter = configValueGetter;
            _coroutineRunner = coroutineRunner;
        }

        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(ruleFactory.GetInstance(_coroutineRunner));
        }

        protected override IEnumerable<IScopeComposer> GetPartialScopeComposers()
        {
            return base
                .GetPartialScopeComposers()
                .Append(new LoggingComposer())
                .Append(new ScreenLoadingComposer(_screenDefinitionGetter, _rootScreenPlacement))
                .Append(new ConfiguringComposer(_configValueGetter))
                .Append(new TweeningComposer());
        }

        protected override IEnumerable<IScopeComposer> GetChildScopeComposers()
        {
            return base.GetChildScopeComposers().Append(new ModelViewViewModelComposer());
        }
    }
}