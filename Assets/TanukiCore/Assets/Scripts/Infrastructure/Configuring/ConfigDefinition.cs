using System;
using UnityEngine;

namespace Infrastructure.Configuring
{
    [Serializable]
    public class ConfigDefinition : IConfigDefinition
    {
        [SerializeField] private string _configKey;
        [SerializeField] private string _value;

        public string ConfigKey => _configKey;

        public string Value => _value;
    }
}