using Game.Gameplay.EventEnqueueing;
using Game.Gameplay.EventEnqueueing.Events;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolution
{
    public class EventListener : IEventListener
    {
        [NotNull] private readonly IEventDequeuer _eventDequeuer;
        [NotNull] private readonly IEventsResolver _eventsResolver;

        private bool _resolving;

        public EventListener([NotNull] IEventDequeuer eventDequeuer, [NotNull] IEventsResolver eventsResolver)
        {
            ArgumentNullException.ThrowIfNull(eventDequeuer);
            ArgumentNullException.ThrowIfNull(eventsResolver);

            _eventDequeuer = eventDequeuer;
            _eventsResolver = eventsResolver;
        }

        public void Initialize()
        {
            // TODO: Check allow multiple Initialize. Add Clear ¿?

            ResolveOrStartListening();
        }

        private void ResolveOrStartListening()
        {
            if (_eventDequeuer.TryDequeue(out IEvent evt))
            {
                Resolve(evt);
            }
            else
            {
                StartListening();
            }
        }

        private void Resolve(IEvent evt)
        {
            StopListening();

            if (_resolving)
            {
                InvalidOperationException.Throw("Resolve is already in progress");
            }

            _resolving = true;

            _eventsResolver.Resolve(
                evt,
                () =>
                {
                    _resolving = false;

                    ResolveOrStartListening();
                }
            );
        }

        private void StartListening()
        {
            StopListening();

            _eventDequeuer.OnEventToDequeue += ResolveOrStartListening;
        }

        private void StopListening()
        {
            _eventDequeuer.OnEventToDequeue -= ResolveOrStartListening;
        }
    }
}