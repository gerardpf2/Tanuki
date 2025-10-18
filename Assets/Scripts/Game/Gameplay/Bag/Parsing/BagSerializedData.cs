using System.Collections.Generic;
using Game.Gameplay.Board;
using Unity.Plastic.Newtonsoft.Json;

namespace Game.Gameplay.Bag.Parsing
{
    public class BagSerializedData
    {
        [JsonProperty("ENTRIES", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public List<BagPieceEntrySerializedData> BagPieceEntries { get; set; }

        [JsonProperty("INITIAL", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public List<PieceType> InitialPieceTypes { get; set; }
    }
}