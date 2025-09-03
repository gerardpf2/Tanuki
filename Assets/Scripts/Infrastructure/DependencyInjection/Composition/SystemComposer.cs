using Infrastructure.System;
using Infrastructure.System.Exceptions;
using Infrastructure.System.Parsing;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.Composition
{
    public class SystemComposer : ScopeComposer
    {
        // SystemComposer should be inside System assembly, but it is not possible because of a circular dependency

        [NotNull] private readonly IConverter _converter;

        public SystemComposer([NotNull] IConverter converter)
        {
            ArgumentNullException.ThrowIfNull(converter);

            _converter = converter;
        }

        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(ruleFactory.GetSingleton<IParser>(_ => new JsonParser()));

            ruleAdder.Add(ruleFactory.GetInstance(_converter));
        }
    }
}