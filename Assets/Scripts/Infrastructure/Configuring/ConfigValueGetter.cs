using Infrastructure.System;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Infrastructure.Configuring
{
    public class ConfigValueGetter : IConfigValueGetter
    {
        [NotNull] private readonly IConfigDefinitionGetter _configDefinitionGetter;
        [NotNull] private readonly IConverter _converter;

        public ConfigValueGetter(
            [NotNull] IConfigDefinitionGetter configDefinitionGetter,
            [NotNull] IConverter converter)
        {
            ArgumentNullException.ThrowIfNull(configDefinitionGetter);
            ArgumentNullException.ThrowIfNull(converter);

            _configDefinitionGetter = configDefinitionGetter;
            _converter = converter;
        }

        public T Get<T>(string configKey)
        {
            IConfigDefinition configDefinition = _configDefinitionGetter.Get(configKey);

            return _converter.Convert<T>(configDefinition.Value);
        }
    }
}