using Game.Gameplay.EventEnqueueing;
using Game.Gameplay.EventEnqueueing.Events;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolution
{
    public class EventListener : IEventListener
    {
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventsResolver _eventsResolver;

        private bool _resolving;

        public EventListener([NotNull] IEventEnqueuer eventEnqueuer, [NotNull] IEventsResolver eventsResolver)
        {
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventsResolver);

            _eventEnqueuer = eventEnqueuer;
            _eventsResolver = eventsResolver;
        }

        public void Initialize()
        {
            Uninitialize();

            ResolveOrStartListening();
        }

        public void Uninitialize()
        {
            StopListening();
        }

        private void ResolveOrStartListening()
        {
            if (_eventEnqueuer.TryDequeue(out IEvent evt))
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

            _eventEnqueuer.OnEventToDequeue += ResolveOrStartListening;
        }

        private void StopListening()
        {
            _eventEnqueuer.OnEventToDequeue -= ResolveOrStartListening;
        }
    }
}