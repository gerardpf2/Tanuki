using JetBrains.Annotations;

namespace Infrastructure.Configuring
{
    public interface IConfigDefinitionGetter
    {
        [NotNull]
        IConfigDefinition Get(string configKey);
    }
}