using System;
using UnityEngine;

namespace Infrastructure.Configuring
{
    [Serializable]
    public class ConfigDefinition : IConfigDefinition
    {
        [SerializeField] private string _configKey;
        [SerializeField] private TypeCode _typeCode;
        [SerializeField] private string _value;

        public string ConfigKey => _configKey;

        public TypeCode TypeCode => _typeCode;

        public string Value => _value;
    }
}