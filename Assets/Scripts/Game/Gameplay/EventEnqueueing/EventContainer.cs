using System;
using System.Collections.Generic;
using Game.Gameplay.EventEnqueueing.Events;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.EventEnqueueing
{
    public class EventContainer : IEventEnqueuer, IEventDequeuer
    {
        [NotNull, ItemNotNull] private readonly Queue<IEvent> _events = new();

        public event Action OnEventToDequeue;

        public void Enqueue([NotNull] IEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            _events.Enqueue(evt);

            OnEventToDequeue?.Invoke();
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