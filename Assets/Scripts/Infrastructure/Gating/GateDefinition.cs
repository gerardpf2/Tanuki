using System;
using Infrastructure.System;
using UnityEngine;

namespace Infrastructure.Gating
{
    [Serializable]
    public class GateDefinition
    {
        [SerializeField] private string _gateKey;

        [SerializeField] private bool _useConfig;
        [SerializeField] private string _config; // TODO: Show / hide based on _useConfig

        [SerializeField] private bool _useVersion;
        [SerializeField] private string _version; // TODO: Show / hide based on _useVersion
        [SerializeField] private ComparisonOperator _versionComparisonOperator; // TODO: Show / hide based on _useVersion

        public string GateKey => _gateKey;

        public bool UseConfig => _useConfig;

        public string Config => _config;

        public bool UseVersion => _useVersion;

        public string Version => _version;

        public ComparisonOperator VersionComparisonOperator => _versionComparisonOperator;
    }
}