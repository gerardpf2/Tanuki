using JetBrains.Annotations;

namespace Infrastructure.Configuring
{
    public interface IConfigValueGetter
    {
        [NotNull]
        T Get<T>(string configKey);
    }
}