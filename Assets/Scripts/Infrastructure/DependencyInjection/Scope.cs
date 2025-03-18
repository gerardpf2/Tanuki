using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class Scope
    {
        [NotNull]
        public virtual IEnumerable<PartialScope> PartialScopes => _partialScopes;

        [NotNull]
        public virtual IEnumerable<Scope> ChildScopes => _childScopes;

        public readonly IRuleAdder RuleAdder;
        public readonly IRuleResolver RuleResolver;
        public readonly Action<IRuleResolver> Initialize;

        [NotNull] private readonly HashSet<PartialScope> _partialScopes = new();
        [NotNull] private readonly HashSet<Scope> _childScopes = new();

        public Scope(IRuleAdder ruleAdder, IRuleResolver ruleResolver, Action<IRuleResolver> initialize)
        {
            RuleAdder = ruleAdder;
            RuleResolver = ruleResolver;
            Initialize = initialize;
        }

        public virtual void AddPartial(PartialScope partialScope)
        {
            _partialScopes.Add(partialScope);
        }

        public virtual void AddChild(Scope childScope)
        {
            _childScopes.Add(childScope);
        }
    }
}