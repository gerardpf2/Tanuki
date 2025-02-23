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
            scopeBuildingContext.SetInitialize(Initialize);
            scopeBuildingContext.SetGetChildScopeComposers(GetChildScopeComposers);
        }

        protected virtual void AddRules(IRuleContainer ruleContainer) { }

        protected virtual void Initialize(IRuleResolver ruleResolver) { }

        protected virtual IEnumerable<IScopeComposer> GetChildScopeComposers()
        {
            return Enumerable.Empty<IScopeComposer>();
        }
    }
}