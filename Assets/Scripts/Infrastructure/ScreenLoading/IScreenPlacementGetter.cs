using JetBrains.Annotations;

namespace Infrastructure.ScreenLoading
{
    public interface IScreenPlacementGetter
    {
        [NotNull]
        IScreenPlacement Get(string key);
    }
}