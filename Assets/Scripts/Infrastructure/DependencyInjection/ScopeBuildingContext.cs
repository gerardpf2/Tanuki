using System;
using System.Collections.Generic;

namespace Infrastructure.DependencyInjection
{
    public class ScopeBuildingContext
    {
        public Func<string> GetGateKey { get; set; }

        public Action<IRuleAdder, IRuleFactory> AddPrivateRules { get; set; }

        public Action<IRuleAdder, IRuleFactory> AddPublicRules { get; set; }

        public Action<IRuleAdder, IRuleFactory> AddGlobalRules { get; set; }

        public Func<IRuleResolver, IEnumerable<IScopeComposer>> GetPartialScopeComposers { get; set; }

        public Func<IRuleResolver, IEnumerable<IScopeComposer>> GetChildScopeComposers { get; set; }

        public Action<IRuleResolver> Initialize { get; set; }
    }
}