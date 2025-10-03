using System.Collections.Generic;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class DamagePieceEvent : IEvent
    {
        public readonly int Id;
        public readonly IEnumerable<KeyValuePair<string, string>> State;

        public DamagePieceEvent(int id, IEnumerable<KeyValuePair<string, string>> state)
        {
            Id = id;
            State = state;
        }
    }
}