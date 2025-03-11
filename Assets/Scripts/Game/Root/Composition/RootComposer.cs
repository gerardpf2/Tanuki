using System.Collections.Generic;
using System.Linq;
using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel.Composition;

namespace Game.Root.Composition
{
    // TODO: Test
    public class RootComposer : ScopeComposer
    {
        protected override IEnumerable<IScopeComposer> GetChildScopeComposers()
        {
            return base.GetChildScopeComposers().Append(new ModelViewViewModelComposer());
        }
    }
}