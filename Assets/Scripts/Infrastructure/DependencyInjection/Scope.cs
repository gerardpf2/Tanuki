using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.DependencyInjection
{
    public class Scope
    {
        [NotNull, ItemNotNull] // TODO: Check ItemNotNull ¿?
        public virtual IEnumerable<PartialScope> GetPartialScopes()
        {
            return _partialScopes;
        }

        [NotNull, ItemNotNull] // TODO: Check ItemNotNull ¿?
        public virtual IEnumerable<Scope> GetChildScopes()
        {
            return _childScopes;
        }

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

        public virtual void AddPartial([NotNull] PartialScope partialScope)
        {
            ArgumentNullException.ThrowIfNull(partialScope);

            _partialScopes.Add(partialScope);
        }

        public virtual void AddChild([NotNull] Scope childScope)
        {
            ArgumentNullException.ThrowIfNull(childScope);

            _childScopes.Add(childScope);
        }
    }
}