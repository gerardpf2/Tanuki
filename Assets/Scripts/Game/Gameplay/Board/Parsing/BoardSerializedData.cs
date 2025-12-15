using System.Collections.Generic;
using Game.Gameplay.Pieces.Parsing;
using Unity.Plastic.Newtonsoft.Json;

namespace Game.Gameplay.Board.Parsing
{
    public class BoardSerializedData
    {
        [JsonProperty("COLUMNS", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Columns { get; set; }

        [JsonProperty("PIECES", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public List<PiecePlacementSerializedData> PiecePlacementSerializedData { get; set; }
    }
}