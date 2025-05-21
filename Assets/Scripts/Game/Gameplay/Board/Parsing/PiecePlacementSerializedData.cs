using Unity.Plastic.Newtonsoft.Json;

namespace Game.Gameplay.Board.Parsing
{
    public class PiecePlacementSerializedData
    {
        [JsonProperty("R", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Row { get; set; }

        [JsonProperty("C", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Column { get; set; }

        [JsonProperty("P", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public PieceSerializedData PieceSerializedData { get; set; }
    }
}