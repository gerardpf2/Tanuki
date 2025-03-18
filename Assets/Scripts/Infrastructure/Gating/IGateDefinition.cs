using Infrastructure.System;
using JetBrains.Annotations;

namespace Infrastructure.Gating
{
    public interface IGateDefinition
    {
        string GateKey { get; }

        bool UseConfig { get; }

        string Config { get; }

        bool UseVersion { get; }

        [NotNull]
        string Version { get; }

        ComparisonOperator VersionComparisonOperator { get; }
    }
}