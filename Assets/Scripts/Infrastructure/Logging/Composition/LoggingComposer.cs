using Infrastructure.DependencyInjection;
using JetBrains.Annotations;

namespace Infrastructure.Logging.Composition
{
    public class LoggingComposer : ScopeComposer
    {
        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(ruleFactory.GetSingleton<ILogger>(_ => new Logger()));

            ruleAdder.Add(ruleFactory.GetSingleton(_ => new UnityLogHandler()));
        }

        protected override void Initialize([NotNull] IRuleResolver ruleResolver)
        {
            base.Initialize(ruleResolver);

            ruleResolver.Resolve<ILogger>().Add(ruleResolver.Resolve<UnityLogHandler>());
        }
    }
}