using Game.Gameplay.Board;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Converters;

namespace Game.Gameplay.Goals.Parsing
{
    public class GoalSerializedData
    {
        [JsonProperty("TYPE"), JsonConverter(typeof(StringEnumConverter))]
        public PieceType PieceType { get; set; }

        [JsonProperty("AMOUNT", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int InitialAmount { get; set; }
    }
}