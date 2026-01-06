using Game;
using Infrastructure.Configuring;
using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Rules;
using Infrastructure.Gating;
using Infrastructure.ScreenLoading;
using Infrastructure.System;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using JetBrains.Annotations;
using Root.Composition;

namespace Root.UseCases
{
    public class BuildAndInitializeRootScopeUseCase : IBuildAndInitializeRootScopeUseCase
    {
        [NotNull] private readonly IGateDefinitionGetter _gateDefinitionGetter;
        [NotNull] private readonly IConfigDefinitionGetter _configDefinitionGetter;
        [NotNull] private readonly IScreenGetter _screenGetter;
        [NotNull] private readonly IScreenPlacement _rootScreenPlacement;
        [NotNull] private readonly ICoroutineRunner _coroutineRunner;
        [NotNull] private readonly IGameScopeComposerBuilder _gameScopeComposerBuilder;

        public BuildAndInitializeRootScopeUseCase(
            [NotNull] IGateDefinitionGetter gateDefinitionGetter,
            [NotNull] IConfigDefinitionGetter configDefinitionGetter,
            [NotNull] IScreenGetter screenGetter,
            [NotNull] IScreenPlacement rootScreenPlacement,
            [NotNull] ICoroutineRunner coroutineRunner,
            [NotNull] IGameScopeComposerBuilder gameScopeComposerBuilder)
        {
            ArgumentNullException.ThrowIfNull(gateDefinitionGetter);
            ArgumentNullException.ThrowIfNull(configDefinitionGetter);
            ArgumentNullException.ThrowIfNull(screenGetter);
            ArgumentNullException.ThrowIfNull(rootScreenPlacement);
            ArgumentNullException.ThrowIfNull(coroutineRunner);
            ArgumentNullException.ThrowIfNull(gameScopeComposerBuilder);

            _gateDefinitionGetter = gateDefinitionGetter;
            _configDefinitionGetter = configDefinitionGetter;
            _screenGetter = screenGetter;
            _rootScreenPlacement = rootScreenPlacement;
            _coroutineRunner = coroutineRunner;
            _gameScopeComposerBuilder = gameScopeComposerBuilder;
        }

        public Scope Resolve()
        {
            RuleContainer ruleContainer = new();
            IRuleResolver ruleResolver = new RuleResolver(ruleContainer, null);

            AddRules(ruleContainer);
            Scope scope = Build(ruleResolver);
            Initialize(ruleResolver, scope);

            return scope;
        }

        private void AddRules([NotNull] IRuleAdder ruleAdder)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);

            ruleAdder.Add(new InstanceRule<IGateDefinitionGetter>(_gateDefinitionGetter));

            ruleAdder.Add(new InstanceRule<IConfigDefinitionGetter>(_configDefinitionGetter));

            ruleAdder.Add(new InstanceRule<IScreenGetter>(_screenGetter));

            ruleAdder.Add(new InstanceRule<IScreenPlacement>(_rootScreenPlacement));

            ruleAdder.Add(new InstanceRule<ICoroutineRunner>(_coroutineRunner));

            ruleAdder.Add(new InstanceRule<IGameScopeComposerBuilder>(_gameScopeComposerBuilder));

            ruleAdder.Add(
                new SingletonRule<IConfigValueGetter>(r =>
                    new ConfigValueGetter(
                        r.Resolve<IConfigDefinitionGetter>(),
                        r.Resolve<IConverter>()
                    )
                )
            );

            ruleAdder.Add(
                new SingletonRule<InjectResolver>(r =>
                    new InjectResolver(
                        r.Resolve<IRuleResolver>()
                    )
                )
            );

            ruleAdder.Add(new SingletonRule<RuleContainer>(_ => new RuleContainer()));
            ruleAdder.Add(new ToRule<IRuleAdder, RuleContainer>());
            ruleAdder.Add(new ToRule<IRuleGetter, RuleContainer>());

            ruleAdder.Add(
                new SingletonRule<IRuleFactory>(r =>
                    new RuleFactory(
                        r.Resolve<IGateValidator>()
                    )
                )
            );

            ruleAdder.Add(
                new SingletonRule<IRuleResolver>(r =>
                    new RuleResolver(
                        r.Resolve<IRuleGetter>(),
                        null
                    )
                )
            );

            ruleAdder.Add(
                new SingletonRule<IScopeBuilder>(r =>
                    new ScopeBuilder(
                        r.Resolve<IGateValidator>(),
                        r.Resolve<IScopeConstructor>(),
                        r.Resolve<ISharedRuleAdder>(),
                        r.Resolve<IRuleFactory>()
                    )
                )
            );

            ruleAdder.Add(
                new SingletonRule<IScopeComposer>(r =>
                    new RootComposer(
                        r.Resolve<IScreenGetter>(),
                        r.Resolve<IScreenPlacement>(),
                        r.Resolve<IConfigValueGetter>(),
                        r.Resolve<ICoroutineRunner>(),
                        r.Resolve<IConverter>(),
                        r.Resolve<IGameScopeComposerBuilder>()
                    )
                )
            );

            ruleAdder.Add(new SingletonRule<IScopeConstructor>(_ => new ScopeConstructor()));

            ruleAdder.Add(new SingletonRule<IScopeInitializer>(_ => new ScopeInitializer()));

            ruleAdder.Add(
                new SingletonRule<ISharedRuleAdder>(r =>
                    new SharedRuleAdder(
                        r.Resolve<IRuleAdder>(),
                        r.Resolve<IRuleFactory>()
                    )
                )
            );

            ruleAdder.Add(
                new SingletonRule<IGateValidator>(r =>
                    new GateValidator(
                        r.Resolve<IGateDefinitionGetter>(),
                        configKey => r.Resolve<IConfigValueGetter>().Get<bool>(configKey),
                        r.Resolve<IProjectVersionGetter>().Get(),
                        r.Resolve<IComparer>()
                    )
                )
            );

            ruleAdder.Add(new SingletonRule<IConverter>(_ => new Converter()));

            ruleAdder.Add(new SingletonRule<IComparer>(_ => new Comparer()));

            ruleAdder.Add(new SingletonRule<IProjectVersionGetter>(_ => new ProjectVersionGetter()));
        }

        private static Scope Build([NotNull] IRuleResolver ruleResolver)
        {
            ArgumentNullException.ThrowIfNull(ruleResolver);

            // Master allows root rule resolver to have shared rule resolver as parent

            Scope master = new(null, ruleResolver.Resolve<IRuleResolver>(), null);

            return ruleResolver.Resolve<IScopeBuilder>().Build(master, ruleResolver.Resolve<IScopeComposer>());
        }

        private static void Initialize([NotNull] IRuleResolver ruleResolver, Scope scope)
        {
            ArgumentNullException.ThrowIfNull(ruleResolver);

            ruleResolver.Resolve<InjectResolver>();
            ruleResolver.Resolve<IScopeInitializer>().Initialize(scope);
        }
    }
}