using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Infrastructure.Configuring.Composition
{
    public class ConfiguringComposer : ScopeComposer
    {
        [NotNull] private readonly IConfigValueGetter _configValueGetter;

        public ConfiguringComposer([NotNull] IConfigValueGetter configValueGetter)
        {
            ArgumentNullException.ThrowIfNull(configValueGetter);

            _configValueGetter = configValueGetter;
        }

        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(ruleFactory.GetInstance(_configValueGetter));
        }
    }
}