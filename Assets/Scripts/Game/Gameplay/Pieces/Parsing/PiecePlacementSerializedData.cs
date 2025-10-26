using Unity.Plastic.Newtonsoft.Json;

namespace Game.Gameplay.Pieces.Parsing
{
    public class PiecePlacementSerializedData
    {
        [JsonProperty("ROW", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Row { get; set; }

        [JsonProperty("COLUMN", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Column { get; set; }

        [JsonProperty("PIECE", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public PieceSerializedData PieceSerializedData { get; set; }
    }
}