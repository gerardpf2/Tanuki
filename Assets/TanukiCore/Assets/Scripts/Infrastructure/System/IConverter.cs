using JetBrains.Annotations;

namespace Infrastructure.System
{
    public interface IConverter
    {
        [NotNull]
        T Convert<T>(object value);
    }
}