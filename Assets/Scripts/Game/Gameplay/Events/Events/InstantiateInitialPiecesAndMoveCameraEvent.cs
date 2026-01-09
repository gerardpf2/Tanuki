using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Events.Events
{
    public class InstantiateInitialPiecesAndMoveCameraEvent : IEvent
    {
        [NotNull, ItemNotNull] public readonly IEnumerable<InstantiatePieceEvent> InstantiatePieceEvents;
        [NotNull] public readonly MoveCameraEvent MoveCameraEvent;

        public InstantiateInitialPiecesAndMoveCameraEvent(
            [NotNull, ItemNotNull] IEnumerable<InstantiatePieceEvent> instantiatePieceEvents,
            [NotNull] MoveCameraEvent moveCameraEvent)
        {
            ArgumentNullException.ThrowIfNull(instantiatePieceEvents);
            ArgumentNullException.ThrowIfNull(moveCameraEvent);

            ICollection<InstantiatePieceEvent> instantiatePieceEventsCopy = new List<InstantiatePieceEvent>();

            foreach (InstantiatePieceEvent instantiatePieceEvent in instantiatePieceEvents)
            {
                ArgumentNullException.ThrowIfNull(instantiatePieceEvent);

                instantiatePieceEventsCopy.Add(instantiatePieceEvent);
            }

            InstantiatePieceEvents = instantiatePieceEventsCopy;
            MoveCameraEvent = moveCameraEvent;
        }
    }
}