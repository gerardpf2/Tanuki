using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;

namespace Game.Gameplay.Board.Parsing
{
    public class BoardSerializedData
    {
        [JsonProperty("R", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Rows { get; set; }

        [JsonProperty("C", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Columns { get; set; }

        [JsonProperty("P", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public List<PiecePlacementSerializedData> PiecePlacementSerializedData { get; set; }
    }
}