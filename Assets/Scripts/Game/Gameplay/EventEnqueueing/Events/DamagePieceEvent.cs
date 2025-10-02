using System.Collections.Generic;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class DamagePieceEvent : IEvent
    {
        public readonly uint Id;
        public readonly IEnumerable<KeyValuePair<string, string>> State;

        public DamagePieceEvent(uint id, IEnumerable<KeyValuePair<string, string>> state)
        {
            Id = id;
            State = state;
        }
    }
}