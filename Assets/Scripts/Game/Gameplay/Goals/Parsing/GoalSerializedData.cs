using Game.Common.Pieces;
using Game.Gameplay.Pieces;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Converters;

namespace Game.Gameplay.Goals.Parsing
{
    public class GoalSerializedData
    {
        [JsonProperty("TYPE"), JsonConverter(typeof(StringEnumConverter))]
        public PieceType PieceType { get; set; }

        [JsonProperty("INITIAL", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int InitialAmount { get; set; }

        [JsonProperty("CURRENT", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int CurrentAmount { get; set; }
    }
}