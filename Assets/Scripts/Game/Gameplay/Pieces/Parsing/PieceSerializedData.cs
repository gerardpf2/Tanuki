using System.Collections.Generic;
using Game.Common.Pieces;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Converters;

namespace Game.Gameplay.Pieces.Parsing
{
    public class PieceSerializedData
    {
        [JsonProperty("TYPE"), JsonConverter(typeof(StringEnumConverter))]
        public PieceType PieceType { get; set; }

        [JsonProperty("STATE", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public Dictionary<string, string> State { get; set; }
    }
}