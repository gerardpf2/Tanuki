using Unity.Plastic.Newtonsoft.Json;

namespace Game.Gameplay.Moves.Parsing
{
    public class MovesSerializedData
    {
        [JsonProperty("AMOUNT", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Amount { get; set; }
    }
}