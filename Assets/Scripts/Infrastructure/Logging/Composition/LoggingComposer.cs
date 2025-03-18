using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Logging.Composition
{
    public class LoggingComposer : ScopeComposer
    {
        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(ruleFactory.GetSingleton<ILogger>(_ => new Logger()));

            ruleAdder.Add(ruleFactory.GetSingleton(_ => new UnityLogHandler(Debug.unityLogger)));
        }

        protected override void Initialize([NotNull] IRuleResolver ruleResolver)
        {
            ArgumentNullException.ThrowIfNull(ruleResolver);

            base.Initialize(ruleResolver);

            ruleResolver.Resolve<ILogger>().Add(ruleResolver.Resolve<UnityLogHandler>());
        }
    }
}