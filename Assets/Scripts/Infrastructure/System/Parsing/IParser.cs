namespace Infrastructure.System.Parsing
{
    public interface IParser
    {
        string Serialize<T>(T value);

        T Deserialize<T>(string value);
    }
}