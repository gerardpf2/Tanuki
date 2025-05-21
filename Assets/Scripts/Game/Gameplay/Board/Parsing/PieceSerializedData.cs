using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Converters;

namespace Game.Gameplay.Board.Parsing
{
    public class PieceSerializedData
    {
        [JsonProperty("P"), JsonConverter(typeof(StringEnumConverter))]
        public PieceType PieceType { get; set; }

        [JsonProperty("M", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public Dictionary<string, object> Metadata { get; set; }
    }
}