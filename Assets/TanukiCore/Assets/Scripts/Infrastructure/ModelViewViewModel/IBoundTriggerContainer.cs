using JetBrains.Annotations;

namespace Infrastructure.ModelViewViewModel
{
    public interface IBoundTriggerContainer
    {
        void Add<T>(IBoundTrigger<T> boundTrigger);

        [NotNull]
        IBoundTrigger<T> Get<T>(string key);
    }
}