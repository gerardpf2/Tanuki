namespace Infrastructure.ScreenLoading
{
    public interface IScreenLoader
    {
        void Load<T>(string key, T data);
    }
}