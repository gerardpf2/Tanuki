using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.DependencyInjection
{
    public class PartialScope : Scope
    {
        public override IEnumerable<PartialScope> PartialScopes => throw new NotSupportedException();

        public override IEnumerable<Scope> ChildScopes => throw new NotSupportedException();

        [NotNull] private readonly Scope _mainScope;

        public PartialScope(
            [NotNull] PartialScope partialScope,
            IRuleAdder ruleAdder,
            IRuleResolver ruleResolver,
            Action<IRuleResolver> initialize) : this(partialScope._mainScope, ruleAdder, ruleResolver, initialize) { }

        public PartialScope(
            [NotNull] Scope mainScope,
            IRuleAdder ruleAdder,
            IRuleResolver ruleResolver,
            Action<IRuleResolver> initialize) : base(ruleAdder, ruleResolver, initialize)
        {
            ArgumentNullException.ThrowIfNull(mainScope);

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