using Game.Gameplay.Board;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Converters;

namespace Game.Gameplay.Goals.Parsing
{
    public class GoalSerializedData
    {
        [JsonProperty("P"), JsonConverter(typeof(StringEnumConverter))]
        public PieceType PieceType { get; set; }

        [JsonProperty("A", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Amount { get; set; }
    }
}