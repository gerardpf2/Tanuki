using JetBrains.Annotations;

namespace Infrastructure.ScreenLoading
{
    public interface IScreenDefinitionGetter
    {
        [NotNull]
        IScreen Get(string key);
    }
}