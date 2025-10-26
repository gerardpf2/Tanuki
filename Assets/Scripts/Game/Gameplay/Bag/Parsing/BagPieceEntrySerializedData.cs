using Game.Common.Pieces;
using Game.Gameplay.Pieces;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Converters;

namespace Game.Gameplay.Bag.Parsing
{
    public class BagPieceEntrySerializedData
    {
        [JsonProperty("TYPE"), JsonConverter(typeof(StringEnumConverter))]
        public PieceType PieceType { get; set; }

        [JsonProperty("AMOUNT", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Amount { get; set; }
    }
}