using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class PartialScope : Scope
    {
        public override IEnumerable<PartialScope> PartialScopes => throw new NotSupportedException($"Use {nameof(MainScope)}.{nameof(MainScope.PartialScopes)} instead");

        public override IEnumerable<Scope> ChildScopes => throw new NotSupportedException($"Use {nameof(MainScope)}.{nameof(MainScope.ChildScopes)} instead");

        public readonly Scope MainScope;

        public PartialScope([NotNull] PartialScope partialScope, Action<IRuleResolver> initialize) : this(partialScope.MainScope, initialize) { }

        public PartialScope([NotNull] Scope mainScope, Action<IRuleResolver> initialize) : base(mainScope.RuleAdder, mainScope.RuleResolver, initialize)
        {
            MainScope = mainScope;
        }

        public override void AddPartial(PartialScope partialScope)
        {
            MainScope.AddPartial(partialScope);
        }

        public override void AddChild(Scope childScope)
        {
            MainScope.AddChild(childScope);
        }
    }
}