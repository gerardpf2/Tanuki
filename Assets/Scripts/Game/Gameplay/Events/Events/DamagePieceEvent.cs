using System.Collections.Generic;
using Game.Common;
using Game.Gameplay.Board;
using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.Events.Events
{
    public class DamagePieceEvent : IEvent
    {
        public readonly DestroyPieceEvent DestroyPieceEvent;
        public readonly int PieceId;
        public readonly IEnumerable<KeyValuePair<string, string>> State;
        public readonly Coordinate Coordinate;
        public readonly DamagePieceReason DamagePieceReason;
        public readonly Direction Direction;

        public DamagePieceEvent(
            DestroyPieceEvent destroyPieceEvent,
            int pieceId,
            IEnumerable<KeyValuePair<string, string>> state,
            Coordinate coordinate,
            DamagePieceReason damagePieceReason,
            Direction direction)
        {
            DestroyPieceEvent = destroyPieceEvent;
            PieceId = pieceId;
            State = state;
            Coordinate = coordinate;
            DamagePieceReason = damagePieceReason;
            Direction = direction;
        }
    }
}