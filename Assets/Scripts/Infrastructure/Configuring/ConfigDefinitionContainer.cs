using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Infrastructure.Configuring
{
    [CreateAssetMenu(fileName = nameof(ConfigDefinitionContainer), menuName = "Tanuki/Infrastructure/Configuring/" + nameof(ConfigDefinitionContainer))]
    public class ConfigDefinitionContainer : ScriptableObject, IConfigDefinitionGetter
    {
        [SerializeField] private ConfigDefinition[] _configDefinitions;

        public IConfigDefinition Get(string configKey)
        {
            InvalidOperationException.ThrowIfNull(_configDefinitions);

            IConfigDefinition configDefinition = null;

            foreach (ConfigDefinition configDefinitionCandidate in _configDefinitions)
            {
                InvalidOperationException.ThrowIfNull(configDefinitionCandidate);

                if (configDefinitionCandidate.ConfigKey != configKey)
                {
                    continue;
                }

                configDefinition = configDefinitionCandidate;

                break;
            }

            InvalidOperationException.ThrowIfNullWithMessage(
                configDefinition,
                $"Cannot get config definition with ConfigKey: {configKey}"
            );

            return configDefinition;
        }
    }
}