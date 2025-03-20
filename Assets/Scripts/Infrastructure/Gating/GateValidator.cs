using System;
using Infrastructure.Configuring;
using Infrastructure.System;
using Infrastructure.Unity;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;

namespace Infrastructure.Gating
{
    public class GateValidator : IGateValidator
    {
        [NotNull] private readonly IGateDefinitionGetter _gateDefinitionGetter;
        [NotNull] private readonly IConfigValueGetter _configValueGetter;
        [NotNull] private readonly Version _projectVersion;

        public GateValidator(
            [NotNull] IGateDefinitionGetter gateDefinitionGetter,
            [NotNull] IConfigValueGetter configValueGetter,
            [NotNull] IProjectVersionGetter projectVersionGetter)
        {
            ArgumentNullException.ThrowIfNull(gateDefinitionGetter);
            ArgumentNullException.ThrowIfNull(configValueGetter);
            ArgumentNullException.ThrowIfNull(projectVersionGetter);

            _gateDefinitionGetter = gateDefinitionGetter;
            _configValueGetter = configValueGetter;
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

        // TODO: Test
        private bool ValidateConfig(string configKey)
        {
            return _configValueGetter.Get<bool>(configKey);
        }

        private bool ValidateVersion([NotNull] string version, ComparisonOperator versionComparisonOperator)
        {
            ArgumentNullException.ThrowIfNull(version);

            Version gateVersion = Version.Parse(version);

            switch (versionComparisonOperator)
            {
                case ComparisonOperator.EqualTo:
                    return _projectVersion == gateVersion;
                case ComparisonOperator.UnequalTo:
                    return _projectVersion != gateVersion;
                case ComparisonOperator.LessThan:
                    return _projectVersion < gateVersion;
                case ComparisonOperator.GreaterThan:
                    return _projectVersion > gateVersion;
                case ComparisonOperator.LessThanOrEqualTo:
                    return _projectVersion <= gateVersion;
                case ComparisonOperator.GreaterThanOrEqualTo:
                    return _projectVersion >= gateVersion;
                default:
                    ArgumentOutOfRangeException.Throw(versionComparisonOperator);
                    return false;
            }
        }
    }
}