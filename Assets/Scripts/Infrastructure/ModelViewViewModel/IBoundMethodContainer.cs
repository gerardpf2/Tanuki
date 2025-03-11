namespace Infrastructure.ModelViewViewModel
{
    public interface IBoundMethodContainer
    {
        void Add(IBoundMethod boundMethod);

        IBoundMethod Get(string key);
    }
}