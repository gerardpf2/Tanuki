using System.Collections.Generic;
using System.Linq;
using Infrastructure.DependencyInjection;
using Infrastructure.Logging.Composition;
using Infrastructure.ModelViewViewModel.Composition;

namespace Game.Root.Composition
{
    public class RootComposer : ScopeComposer
    {
        protected override IEnumerable<IScopeComposer> GetPartialScopeComposers()
        {
            return base.GetPartialScopeComposers().Append(new LoggingComposer());
        }

        protected override IEnumerable<IScopeComposer> GetChildScopeComposers()
        {
            return base.GetChildScopeComposers().Append(new ModelViewViewModelComposer());
        }
    }
}