using JetBrains.Annotations;

namespace Infrastructure.Gating
{
    public interface IGateDefinitionGetter
    {
        [NotNull]
        GateDefinition Get(string key);
    }
}