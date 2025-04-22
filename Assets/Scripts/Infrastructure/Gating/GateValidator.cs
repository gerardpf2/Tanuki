using System;
using Infrastructure.System;
using Infrastructure.Unity;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.Gating
{
    public class GateValidator : IGateValidator
    {
        [NotNull] private readonly IGateDefinitionGetter _gateDefinitionGetter;
        [NotNull] private readonly Func<string, bool> _configValueGetter;
        [NotNull] private readonly IComparer _comparer;
        [NotNull] private readonly Version _projectVersion;

        public GateValidator(
            [NotNull] IGateDefinitionGetter gateDefinitionGetter,
            [NotNull] Func<string, bool> configValueGetter, // IConfigValueGetter creates a cyclic dependency between assemblies
            [NotNull] IProjectVersionGetter projectVersionGetter,
            [NotNull] IComparer comparer)
        {
            ArgumentNullException.ThrowIfNull(gateDefinitionGetter);
            ArgumentNullException.ThrowIfNull(configValueGetter);
            ArgumentNullException.ThrowIfNull(projectVersionGetter);
            ArgumentNullException.ThrowIfNull(comparer);

            _gateDefinitionGetter = gateDefinitionGetter;
            _configValueGetter = configValueGetter;
            _comparer = comparer;
            _projectVersion = Version.Parse(projectVersionGetter.Get());
        }

        public bool Validate(string gateKey)
        {
            if (gateKey is null)
            {
                return true;
            }

            IGateDefinition gateDefinition = _gateDefinitionGetter.Get(gateKey);

            return
                (!gateDefinition.UseConfig || ValidateConfig(gateDefinition.ConfigKey)) &&
                (!gateDefinition.UseVersion || ValidateVersion(gateDefinition.Version, gateDefinition.VersionComparisonOperator));
        }

        private bool ValidateConfig(string configKey)
        {
            return _configValueGetter(configKey);
        }

        private bool ValidateVersion([NotNull] string version, ComparisonOperator versionComparisonOperator)
        {
            ArgumentNullException.ThrowIfNull(version);

            Version gateVersion = Version.Parse(version);

            return _comparer.IsTrueThat(_projectVersion, versionComparisonOperator, gateVersion);
        }
    }
}