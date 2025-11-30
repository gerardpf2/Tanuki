using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Events.Events
{
    public class DamagePiecesByLineClearEvent : IEvent
    {
        [NotNull, ItemNotNull] public readonly IEnumerable<DamagePieceEvent> DamagePieceEvents;

        [NotNull] private readonly ICollection<int> _pieceIds = new HashSet<int>();

        public DamagePiecesByLineClearEvent([NotNull, ItemNotNull] IEnumerable<DamagePieceEvent> damagePieceEvents)
        {
            ArgumentNullException.ThrowIfNull(damagePieceEvents);

            ICollection<DamagePieceEvent> damagePieceEventsCopy = new List<DamagePieceEvent>();

            foreach (DamagePieceEvent damagePieceEvent in damagePieceEvents)
            {
                ArgumentNullException.ThrowIfNull(damagePieceEvent);

                int pieceId = damagePieceEvent.PieceId;

                if (_pieceIds.Contains(pieceId))
                {
                    InvalidOperationException.Throw($"Piece with Id: {pieceId} has already been added");
                }
                else
                {
                    _pieceIds.Add(pieceId);
                }

                damagePieceEventsCopy.Add(damagePieceEvent);
            }

            DamagePieceEvents = damagePieceEventsCopy;
        }
    }
}