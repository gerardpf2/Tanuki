using System;
using System.Collections.Generic;
using Infrastructure.Gating;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Infrastructure.DependencyInjection
{
    public class ScopeBuilder : IScopeBuilder
    {
        [NotNull] private readonly IGateValidator _gateValidator;
        [NotNull] private readonly IScopeConstructor _scopeConstructor;
        [NotNull] private readonly ISharedRuleAdder _sharedRuleAdder;
        private readonly IRuleFactory _ruleFactory;

        public ScopeBuilder(
            [NotNull] IGateValidator gateValidator,
            [NotNull] IScopeConstructor scopeConstructor,
            [NotNull] ISharedRuleAdder sharedRuleAdder,
            IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(gateValidator);
            ArgumentNullException.ThrowIfNull(scopeConstructor);
            ArgumentNullException.ThrowIfNull(sharedRuleAdder);

            _gateValidator = gateValidator;
            _scopeConstructor = scopeConstructor;
            _sharedRuleAdder = sharedRuleAdder;
            _ruleFactory = ruleFactory;
        }

        public PartialScope BuildPartial(Scope mainScope, [NotNull] IScopeComposer scopeComposer)
        {
            ArgumentNullException.ThrowIfNull(scopeComposer);

            return
                Build(
                    scopeComposer,
                    initialize => _scopeConstructor.ConstructPartial(mainScope, initialize)
                );
        }

        public Scope Build(Scope parentScope, [NotNull] IScopeComposer scopeComposer)
        {
            ArgumentNullException.ThrowIfNull(scopeComposer);

            return
                Build(
                    scopeComposer,
                    initialize => _scopeConstructor.Construct(parentScope, initialize)
                );
        }

        private T Build<T>([NotNull] IScopeComposer scopeComposer, [NotNull] Func<Action<IRuleResolver>, T> ctor) where T : Scope
        {
            ArgumentNullException.ThrowIfNull(scopeComposer);
            ArgumentNullException.ThrowIfNull(ctor);

            ScopeBuildingContext scopeBuildingContext = new();

            scopeComposer.Compose(scopeBuildingContext);

            if (!_gateValidator.Validate(scopeBuildingContext.GetGateKey?.Invoke()))
            {
                return null;
            }

            T scope = ctor(scopeBuildingContext.Initialize);

            if (scope is null)
            {
                return null;
            }

            AddRules(scope, scopeBuildingContext.AddRules);
            AddSharedRules(scope, scopeBuildingContext.AddSharedRules);
            BuildPartialScopeComposers(scope, scopeBuildingContext.GetPartialScopeComposers);
            BuildChildScopeComposers(scope, scopeBuildingContext.GetChildScopeComposers);

            return scope;
        }

        private void AddRules([NotNull] Scope scope, Action<IRuleAdder, IRuleFactory> addRules)
        {
            ArgumentNullException.ThrowIfNull(scope);

            addRules?.Invoke(scope.RuleAdder, _ruleFactory);
        }

        private void AddSharedRules([NotNull] Scope scope, Action<IRuleAdder, IRuleFactory> addSharedRules)
        {
            ArgumentNullException.ThrowIfNull(scope);

            _sharedRuleAdder.SetTarget(scope.RuleAdder, scope.RuleResolver);

            addSharedRules?.Invoke(_sharedRuleAdder, _ruleFactory);

            _sharedRuleAdder.ClearTarget();
        }

        private void BuildPartialScopeComposers(
            Scope mainScope,
            Func<IEnumerable<IScopeComposer>> getPartialScopeComposers)
        {
            IEnumerable<IScopeComposer> partialScopeComposers = getPartialScopeComposers?.Invoke();

            if (partialScopeComposers is null)
            {
                return;
            }

            foreach (IScopeComposer partialScopeComposer in partialScopeComposers)
            {
                InvalidOperationException.ThrowIfNull(partialScopeComposer);

                BuildPartial(mainScope, partialScopeComposer);
            }
        }

        private void BuildChildScopeComposers(
            Scope parentScope,
            Func<IEnumerable<IScopeComposer>> getChildScopeComposers)
        {
            IEnumerable<IScopeComposer> childScopeComposers = getChildScopeComposers?.Invoke();

            if (childScopeComposers is null)
            {
                return;
            }

            foreach (IScopeComposer childScopeComposer in childScopeComposers)
            {
                InvalidOperationException.ThrowIfNull(childScopeComposer);

                Build(parentScope, childScopeComposer);
            }
        }
    }
}