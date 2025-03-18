using JetBrains.Annotations;

namespace Infrastructure.ScreenLoading
{
    public interface IScreenDefinitionGetter
    {
        [NotNull]
        IScreenDefinition Get(string key);
    }
}