using JetBrains.Annotations;

namespace Infrastructure.ScreenLoading
{
    public interface IScreenGetter
    {
        [NotNull]
        IScreen Get(string key);
    }
}