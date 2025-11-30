using System.Collections.Generic;
using Game.Gameplay.Events.Reasons;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Events.Events
{
    public class MovePiecesByGravityEvent : IEvent
    {
        [NotNull, ItemNotNull] public readonly IEnumerable<MovePieceEvent> MovePieceEvents;

        [NotNull] private readonly ICollection<int> _pieceIds = new HashSet<int>();

        public MovePiecesByGravityEvent([NotNull] IEnumerable<KeyValuePair<int, int>> fallData)
        {
            ArgumentNullException.ThrowIfNull(fallData);

            ICollection<MovePieceEvent> movePieceEvents = new List<MovePieceEvent>();

            foreach ((int pieceId, int fall) in fallData)
            {
                if (_pieceIds.Contains(pieceId))
                {
                    InvalidOperationException.Throw($"Piece with Id: {pieceId} has already been added");
                }

                _pieceIds.Add(pieceId);

                movePieceEvents.Add(GetMovePieceEvent(pieceId, fall));
            }

            MovePieceEvents = movePieceEvents;
        }

        [NotNull]
        private static MovePieceEvent GetMovePieceEvent(int pieceId, int fall)
        {
            int rowOffset = -fall;
            const int columnOffset = 0;

            return new MovePieceEvent(pieceId, rowOffset, columnOffset, MovePieceReason.Gravity);
        }
    }
}