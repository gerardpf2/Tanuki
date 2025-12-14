using JetBrains.Annotations;

namespace Infrastructure.Unity
{
    public interface IProjectVersionGetter
    {
        [NotNull]
        string Get();
    }
}