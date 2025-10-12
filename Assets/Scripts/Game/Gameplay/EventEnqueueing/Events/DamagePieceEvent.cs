using System.Collections.Generic;
using Game.Gameplay.EventEnqueueing.Events.Reasons;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class DamagePieceEvent : IEvent
    {
        public readonly int Id;
        public readonly IEnumerable<KeyValuePair<string, string>> State;
        public readonly DamagePieceReason DamagePieceReason;

        public DamagePieceEvent(
            int id,
            IEnumerable<KeyValuePair<string, string>> state,
            DamagePieceReason damagePieceReason)
        {
            Id = id;
            State = state;
            DamagePieceReason = damagePieceReason;
        }
    }
}