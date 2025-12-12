namespace Infrastructure.ModelViewViewModel
{
    public interface IBoundMethod
    {
        string Key { get; }

        void Call();
    }
}