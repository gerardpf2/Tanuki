using System;
using Infrastructure.System;
using Infrastructure.Unity;
using UnityEngine;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Infrastructure.Gating
{
    [Serializable]
    public class GateDefinition : IGateDefinition
    {
        [SerializeField] private string _gateKey;

        [SerializeField] private bool _useConfig;
        [SerializeField, ShowInInspectorIf(nameof(_useConfig))] private string _configKey;

        [SerializeField] private bool _useVersion;
        [SerializeField, ShowInInspectorIf(nameof(_useVersion))] private string _version = string.Empty;
        [SerializeField, ShowInInspectorIf(nameof(_useVersion))] private ComparisonOperator _versionComparisonOperator;

        public string GateKey => _gateKey;

        public bool UseConfig => _useConfig;

        public string ConfigKey => _configKey;

        public bool UseVersion => _useVersion;

        public string Version
        {
            get
            {
                InvalidOperationException.ThrowIfNull(_version);

                return _version;
            }
        }

        public ComparisonOperator VersionComparisonOperator => _versionComparisonOperator;
    }
}