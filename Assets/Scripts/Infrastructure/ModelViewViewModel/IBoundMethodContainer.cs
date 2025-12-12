using JetBrains.Annotations;

namespace Infrastructure.ModelViewViewModel
{
    public interface IBoundMethodContainer
    {
        void Add(IBoundMethod boundMethod);

        [NotNull]
        IBoundMethod Get(string key);
    }
}