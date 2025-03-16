using Infrastructure.DependencyInjection;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Logging.Composition
{
    public class LoggingComposer : ScopeComposer
    {
        protected override void AddPublicRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            base.AddPublicRules(ruleAdder, ruleFactory);

            ruleAdder.Add(ruleFactory.GetSingleton<ILogger>(_ => new Logger()));

            ruleAdder.Add(ruleFactory.GetSingleton(_ => new UnityLogHandler(Debug.unityLogger)));
        }

        protected override void Initialize([NotNull] IRuleResolver ruleResolver)
        {
            base.Initialize(ruleResolver);

            ruleResolver.Resolve<ILogger>().Add(ruleResolver.Resolve<UnityLogHandler>());
        }
    }
}