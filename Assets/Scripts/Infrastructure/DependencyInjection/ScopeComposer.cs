using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class ScopeComposer : IScopeComposer
    {
        public void Compose([NotNull] IScopeBuildingContext scopeBuildingContext)
        {
            scopeBuildingContext.SetAddRules(AddRules);
            scopeBuildingContext.SetGetPartialScopeComposers(GetPartialScopeComposers);
            scopeBuildingContext.SetGetChildScopeComposers(GetChildScopeComposers);
            scopeBuildingContext.SetInitialize(Initialize);
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