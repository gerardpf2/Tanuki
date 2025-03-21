using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Configuring
{
    [CreateAssetMenu(fileName = nameof(ConfigDefinitionContainer), menuName = "Tanuki/Infrastructure/Configuring/" + nameof(ConfigDefinitionContainer))]
    public class ConfigDefinitionContainer : ScriptableObject, IConfigDefinitionGetter
    {
        [NotNull] [SerializeField] private List<ConfigDefinition> _configDefinitions = new();

        public IConfigDefinition Get(string configKey)
        {
            IConfigDefinition configDefinition = _configDefinitions.Find(configDefinition => configDefinition.ConfigKey == configKey);

            InvalidOperationException.ThrowIfNullWithMessage(
                configDefinition,
                $"Cannot get config definition with ConfigKey: {configKey}"
            );

            return configDefinition;
        }
    }
}