using System.Collections.Generic;
using Game.Gameplay.Pieces;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Converters;

namespace Game.Gameplay.Bag.Parsing
{
    public class BagSerializedData
    {
        [JsonProperty("ENTRIES", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public List<BagPieceEntrySerializedData> BagPieceEntries { get; set; }

        [JsonProperty("INITIAL", ItemConverterType = typeof(StringEnumConverter), DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public List<PieceType> InitialPieceTypes { get; set; }
    }
}