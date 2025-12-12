using JetBrains.Annotations;

namespace Infrastructure.ModelViewViewModel
{
    public interface IBoundPropertyContainer
    {
        void Add<T>(IBoundProperty<T> boundProperty);

        [NotNull]
        IBoundProperty<T> Get<T>(string key);
    }
}