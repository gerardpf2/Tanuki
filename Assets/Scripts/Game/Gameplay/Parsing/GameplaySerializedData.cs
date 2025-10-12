using Game.Gameplay.Board.Parsing;
using Game.Gameplay.Goals.Parsing;
using Game.Gameplay.Moves.Parsing;
using Unity.Plastic.Newtonsoft.Json;

namespace Game.Gameplay.Parsing
{
    public class GameplaySerializedData
    {
        [JsonProperty("BOARD", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public BoardSerializedData BoardSerializedData { get; set; }

        [JsonProperty("GOALS", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public GoalsSerializedData GoalsSerializedData { get; set; }

        [JsonProperty("MOVES", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public MovesSerializedData MovesSerializedData { get; set; }
    }
}