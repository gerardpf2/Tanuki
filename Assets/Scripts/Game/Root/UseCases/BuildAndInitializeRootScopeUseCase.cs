using Game.Root.Composition;
using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Rules;
using Infrastructure.DependencyInjection.Utils;
using Infrastructure.Gating;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Root.UseCases
{
    public class BuildAndInitializeRootScopeUseCase : IBuildAndInitializeRootScopeUseCase
    {
        private readonly IGateDefinitionGetter _gateDefinitionGetter;

        public BuildAndInitializeRootScopeUseCase(IGateDefinitionGetter gateDefinitionGetter)
        {
            _gateDefinitionGetter = gateDefinitionGetter;
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
            ruleAdder.Add(new InstanceRule<IGateDefinitionGetter>(_gateDefinitionGetter));

            ruleAdder.Add(new SingletonRule<IProjectVersionGetter>(_ => new ProjectVersionGetter()));

            ruleAdder.Add(new SingletonRule<IVersionParser>(_ => new VersionParser()));

            ruleAdder.Add(
                new SingletonRule<IGateValidator>(r =>
                    new GateValidator(
                        r.Resolve<IGateDefinitionGetter>(),
                        r.Resolve<IProjectVersionGetter>(),
                        r.Resolve<IVersionParser>()
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

            ruleAdder.Add(new SingletonRule<IScopeComposer>(_ => new RootComposer()));

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
        }

        private static Scope Build([NotNull] IRuleResolver ruleResolver)
        {
            return ruleResolver.Resolve<IScopeBuilder>().BuildRoot(ruleResolver.Resolve<IScopeComposer>());
        }

        private static void Initialize([NotNull] IRuleResolver ruleResolver, Scope scope)
        {
            ruleResolver.Resolve<InjectResolver>();
            ruleResolver.Resolve<IScopeInitializer>().Initialize(scope);
        }
    }
}