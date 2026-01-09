using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;

namespace Game.Gameplay.Goals.Parsing
{
    public class GoalsSerializedData
    {
        [JsonProperty("GOALS", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public List<GoalSerializedData> GoalSerializedData { get; set; }
    }
}