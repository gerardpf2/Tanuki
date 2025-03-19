using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using NotSupportedException = Infrastructure.System.Exceptions.NotSupportedException;

namespace Infrastructure.DependencyInjection
{
    public class PartialScope : Scope
    {
        [ContractAnnotation("=> halt")]
        public override IEnumerable<PartialScope> GetPartialScopes()
        {
            NotSupportedException.Throw();

            return null;
        }

        [ContractAnnotation("=> halt")]
        public override IEnumerable<Scope> GetChildScopes()
        {
            NotSupportedException.Throw();

            return null;
        }

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
            ArgumentNullException.ThrowIfNull(partialScope);

            _mainScope.AddPartial(partialScope);
        }

        public override void AddChild(Scope childScope)
        {
            ArgumentNullException.ThrowIfNull(childScope);

            _mainScope.AddChild(childScope);
        }
    }
}