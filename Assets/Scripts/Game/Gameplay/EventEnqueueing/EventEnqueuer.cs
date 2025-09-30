using System.Collections.Generic;
using Game.Gameplay.EventEnqueueing.Events;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.EventEnqueueing
{
    public class EventEnqueuer : IEventEnqueuer
    {
        [NotNull, ItemNotNull] private readonly Queue<IEvent> _events = new(); // ItemNotNull as long as all Add check for null

        public void Enqueue([NotNull] IEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            _events.Enqueue(evt);
        }

        public bool TryDequeue(out IEvent evt)
        {
            return _events.TryDequeue(out evt);
        }
    }
}