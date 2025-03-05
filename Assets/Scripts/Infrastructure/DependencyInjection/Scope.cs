using System;
using System.Collections.Generic;

namespace Infrastructure.DependencyInjection
{
    public class Scope
    {
        public virtual IEnumerable<PartialScope> PartialScopes => _partialScopes;

        public virtual IEnumerable<Scope> ChildScopes => _childScopes;

        public readonly IRuleAdder RuleAdder;
        public readonly IRuleResolver RuleResolver;
        public readonly Action<IRuleResolver> Initialize;

        private readonly HashSet<PartialScope> _partialScopes = new();
        private readonly HashSet<Scope> _childScopes = new();

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