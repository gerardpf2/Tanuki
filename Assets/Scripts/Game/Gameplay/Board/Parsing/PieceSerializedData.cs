using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Converters;

namespace Game.Gameplay.Board.Parsing
{
    public class PieceSerializedData
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public PieceType PieceType { get; set; }

        public Dictionary<string, object> Metadata { get; set; }
    }
}