using System.Collections.Generic;
using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.Events.Events
{
    public class DamagePieceEvent : IEvent
    {
        public readonly int PieceId;
        public readonly IEnumerable<KeyValuePair<string, string>> State;
        public readonly DamagePieceReason DamagePieceReason;

        public DamagePieceEvent(
            int pieceId,
            IEnumerable<KeyValuePair<string, string>> state,
            DamagePieceReason damagePieceReason)
        {
            PieceId = pieceId;
            State = state;
            DamagePieceReason = damagePieceReason;
        }
    }
}