using System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Infrastructure.Configuring
{
    public class ConfigValueGetter : IConfigValueGetter
    {
        private readonly IConfigDefinitionGetter _configDefinitionGetter;

        public ConfigValueGetter([NotNull] IConfigDefinitionGetter configDefinitionGetter)
        {
            ArgumentNullException.ThrowIfNull(configDefinitionGetter);

            _configDefinitionGetter = configDefinitionGetter;
        }

        public T Get<T>(string configKey)
        {
            IConfigDefinition configDefinition = _configDefinitionGetter.Get(configKey);
            T value = default;

            try
            {
                value = (T)Convert.ChangeType(configDefinition.Value, typeof(T));
            }
            catch (Exception exception)
            {
                InvalidOperationException.Throw(
                    $"Cannot get value of Type: {typeof(T)} from config definition with ConfigKey: {configKey}. {exception.Message}"
                );
            }

            InvalidOperationException.ThrowIfNull(value);

            return value;
        }
    }
}