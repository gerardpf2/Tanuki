using System;
using Infrastructure.System;
using Infrastructure.Unity;
using JetBrains.Annotations;

namespace Infrastructure.Gating
{
    public class GateValidator : IGateValidator
    {
        [NotNull] private readonly IGateDefinitionGetter _gateDefinitionGetter;
        [NotNull] private readonly Version _projectVersion;

        public GateValidator(
            [NotNull] IGateDefinitionGetter gateDefinitionGetter,
            [NotNull] IProjectVersionGetter projectVersionGetter)
        {
            _gateDefinitionGetter = gateDefinitionGetter;
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
                (!gateDefinition.UseConfig || ValidateConfig(gateDefinition.Config)) &&
                (!gateDefinition.UseVersion || ValidateVersion(gateDefinition.Version, gateDefinition.VersionComparisonOperator));
        }

        // TODO: Test
        // TODO: Add support
        private bool ValidateConfig(string config)
        {
            return true;
        }

        private bool ValidateVersion([NotNull] string version, ComparisonOperator versionComparisonOperator)
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