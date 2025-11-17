using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Events.Events
{
    public class MovePiecesByGravityEvent : IEvent
    {
        [NotNull] public readonly IEnumerable<KeyValuePair<int, int>> FallData;

        public MovePiecesByGravityEvent([NotNull] IEnumerable<KeyValuePair<int, int>> fallData)
        {
            ArgumentNullException.ThrowIfNull(fallData);

            IDictionary<int, int> fallDataCopy = new Dictionary<int, int>();

            foreach ((int pieceId, int fall) in fallData)
            {
                if (!fallDataCopy.TryAdd(pieceId, fall))
                {
                    InvalidOperationException.Throw(); // TODO
                }
            }

            FallData = fallDataCopy;
        }
    }
}