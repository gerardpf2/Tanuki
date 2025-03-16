using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class PartialScope : Scope
    {
        public override IEnumerable<PartialScope> PartialScopes => throw new NotSupportedException();

        public override IEnumerable<Scope> ChildScopes => throw new NotSupportedException();

        private readonly Scope _mainScope;

        public PartialScope(
            [NotNull] PartialScope partialScope,
            IRuleAdder privateRuleAdder,
            IRuleAdder publicRuleAdder,
            IRuleGetter publicRuleGetter,
            IRuleResolver ruleResolver,
            Action<IRuleResolver> initialize) : this(partialScope._mainScope, privateRuleAdder, publicRuleAdder, publicRuleGetter, ruleResolver, initialize) { }

        public PartialScope(
            [NotNull] Scope mainScope,
            IRuleAdder privateRuleAdder,
            IRuleAdder publicRuleAdder,
            IRuleGetter publicRuleGetter,
            IRuleResolver ruleResolver,
            Action<IRuleResolver> initialize) : base(privateRuleAdder, publicRuleAdder, publicRuleGetter, ruleResolver, initialize)
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