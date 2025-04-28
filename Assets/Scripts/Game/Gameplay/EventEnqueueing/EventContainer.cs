using System.Collections.Generic;
using Game.Gameplay.EventEnqueueing.Events;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.EventEnqueueing
{
    public class EventContainer : IEventEnqueuer, IEventDequeuer
    {
        [NotNull, ItemNotNull] private readonly Queue<IEvent> _events = new();

        public void Enqueue([NotNull] IEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            _events.Enqueue(evt);
        }

        public bool TryDequeue(out IEvent evt)
        {
            if (_events.Count > 0)
            {
                evt = _events.Dequeue();

                return true;
            }

            evt = null;

            return false;
        }
    }
}