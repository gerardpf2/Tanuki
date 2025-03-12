using Infrastructure.DependencyInjection;
using Infrastructure.Logging.LogHandlers;
using JetBrains.Annotations;

namespace Infrastructure.Logging.Composition
{
    public class UnityLoggingComposer : ScopeComposer
    {
        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(ruleFactory.GetSingleton(_ => new UnityLogHandler()));
        }
        
        protected override void Initialize([NotNull] IRuleResolver ruleResolver)
        {
            base.Initialize(ruleResolver);

            ruleResolver.Resolve<ILogger>().Add(ruleResolver.Resolve<UnityLogHandler>());
        }
    }
}