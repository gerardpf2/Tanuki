using System;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    // TODO: Test
    public class PartialScope : Scope
    {
        private readonly Scope _mainScope;

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