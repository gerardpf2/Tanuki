using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    // TODO: Test
    public class PartialScope : Scope
    {
        // TODO: Exclude self
        public override IEnumerable<PartialScope> PartialScopes => _mainScope.PartialScopes;

        public override IEnumerable<Scope> ChildScopes => _mainScope.ChildScopes;

        private readonly Scope _mainScope;

        public PartialScope([NotNull] PartialScope partialScope, Action<IRuleResolver> initialize) : this(partialScope._mainScope, initialize) { }

        public PartialScope([NotNull] Scope mainScope, Action<IRuleResolver> initialize) : base(mainScope.RuleAdder, mainScope.RuleResolver, initialize)
        {
            _mainScope = mainScope;
        }

        public override void AddPartial(PartialScope partialScope)
        {
            _mainScope.AddPartial(partialScope);
        }

        public override void AddChild(Scope childScope)
        {
            _mainScope.AddChild(childScope);
        }
    }
}