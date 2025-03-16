using System;
using System.Collections.Generic;

namespace Infrastructure.DependencyInjection
{
    public class ScopeBuildingContext
    {
        public Func<string> GetGateKey { get; set; }

        public Action<IRuleAdder, IRuleFactory> AddRules { get; set; }

        public Action<IRuleAdder, IRuleFactory> AddGlobalRules { get; set; }

        public Func<IEnumerable<IScopeComposer>> GetPartialScopeComposers { get; set; }

        public Func<IEnumerable<IScopeComposer>> GetChildScopeComposers { get; set; }

        public Action<IRuleResolver> Initialize { get; set; }
    }
}