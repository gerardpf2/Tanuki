using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class PartialScope : Scope
    {
        public override IEnumerable<Scope> PartialScopes
        {
            get
            {
                return _mainScope.PartialScopes.Where(partialScope => partialScope != this).Append(_mainScope);
            }
        }

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