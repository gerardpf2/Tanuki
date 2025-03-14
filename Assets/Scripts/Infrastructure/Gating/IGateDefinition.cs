using Infrastructure.System;

namespace Infrastructure.Gating
{
    public interface IGateDefinition
    {
        string GateKey { get; }

        bool UseConfig { get; }

        string Config { get; }

        bool UseVersion { get; }

        string Version { get; }

        ComparisonOperator VersionComparisonOperator { get; }
    }
}