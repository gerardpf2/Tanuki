using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class ScopeComposer : IScopeComposer
    {
        public void Compose([NotNull] ScopeBuildingContext scopeBuildingContext)
        {
            scopeBuildingContext.AddRules = AddRules;
            scopeBuildingContext.GetPartialScopeComposers = GetPartialScopeComposers;
            scopeBuildingContext.GetChildScopeComposers = GetChildScopeComposers;
            scopeBuildingContext.Initialize = Initialize;
        }

        protected virtual void AddRules(IRuleContainer ruleContainer) { }

        protected virtual IEnumerable<IScopeComposer> GetPartialScopeComposers()
        {
            return Enumerable.Empty<IScopeComposer>();
        }

        protected virtual IEnumerable<IScopeComposer> GetChildScopeComposers()
        {
            return Enumerable.Empty<IScopeComposer>();
        }

        protected virtual void Initialize(IRuleResolver ruleResolver) { }
    }
}