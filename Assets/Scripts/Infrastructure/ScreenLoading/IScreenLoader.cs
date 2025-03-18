namespace Infrastructure.ScreenLoading
{
    public interface IScreenLoader
    {
        void Load(string key);

        void Load<T>(string key, T data);
    }
}