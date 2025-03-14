using System;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Infrastructure.Gating
{
    public class GateValidator : IGateValidator
    {
        private readonly IGateDefinitionGetter _gateDefinitionGetter;
        private readonly Version _projectVersion;

        public GateValidator(
            [NotNull] IGateDefinitionGetter gateDefinitionGetter,
            [NotNull] IProjectVersionGetter projectVersionGetter)
        {
            _gateDefinitionGetter = gateDefinitionGetter;
            _projectVersion = Version.Parse(projectVersionGetter.Get());
        }

        public bool Validate(string gateKey)
        {
            if (gateKey == null)
            {
                return true;
            }

            IGateDefinition gateDefinition = _gateDefinitionGetter.Get(gateKey);

            return
                (!gateDefinition.UseConfig || ValidateConfig(gateDefinition.Config)) &&
                (!gateDefinition.UseVersion || ValidateVersion(gateDefinition.Version, gateDefinition.VersionComparisonOperator));
        }

        // TODO: Test
        // TODO: Add support
        private bool ValidateConfig(string config)
        {
            return true;
        }

        private bool ValidateVersion(string version, ComparisonOperator versionComparisonOperator)
        {
            Version gateVersion = Version.Parse(version);

            return versionComparisonOperator switch
            {
                ComparisonOperator.EqualTo => _projectVersion == gateVersion,
                ComparisonOperator.UnequalTo => _projectVersion != gateVersion,
                ComparisonOperator.LessThan => _projectVersion < gateVersion,
                ComparisonOperator.GreaterThan => _projectVersion > gateVersion,
                ComparisonOperator.LessThanOrEqualTo => _projectVersion <= gateVersion,
                ComparisonOperator.GreaterThanOrEqualTo => _projectVersion >= gateVersion,
                _ => throw new ArgumentOutOfRangeException(nameof(versionComparisonOperator), versionComparisonOperator, null)
            };
        }
    }
}