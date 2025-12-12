using Unity.Plastic.Newtonsoft.Json;

namespace Infrastructure.System.Parsing
{
    public class JsonParser : IParser
    {
        public string Serialize<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}