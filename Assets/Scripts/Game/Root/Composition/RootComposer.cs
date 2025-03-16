using System.Collections.Generic;
using System.Linq;
using Infrastructure.DependencyInjection;
using Infrastructure.Logging.Composition;
using Infrastructure.ModelViewViewModel.Composition;

namespace Game.Root.Composition
{
    public class RootComposer : ScopeComposer
    {
        protected override IEnumerable<IScopeComposer> GetPartialScopeComposers(IRuleResolver ruleResolver)
        {
            return base.GetPartialScopeComposers(ruleResolver).Append(new LoggingComposer());
        }

        protected override IEnumerable<IScopeComposer> GetChildScopeComposers(IRuleResolver ruleResolver)
        {
            return base.GetChildScopeComposers(ruleResolver).Append(new ModelViewViewModelComposer());
        }
    }
}