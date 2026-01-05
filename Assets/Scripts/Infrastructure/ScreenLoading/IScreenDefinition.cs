using JetBrains.Annotations;

namespace Infrastructure.ScreenLoading
{
    public interface IScreenDefinition
    {
        string Key { get; }

        [NotNull]
        IScreen Screen { get; }
    }
}