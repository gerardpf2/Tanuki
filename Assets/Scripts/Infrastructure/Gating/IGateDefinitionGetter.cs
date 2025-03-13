using JetBrains.Annotations;

namespace Infrastructure.Gating
{
    public interface IGateDefinitionGetter
    {
        [NotNull]
        IGateDefinition Get(string gateKey);
    }
}