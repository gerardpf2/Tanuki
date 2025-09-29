using Game.Gameplay.EventEnqueueing;
using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.PhaseResolution;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolution
{
    public class EventListener : IEventListener
    {
        [NotNull] private readonly IPhaseResolver _phaseResolver;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventsResolver _eventsResolver;

        public bool Resolving { get; private set; }

        public EventListener(
            [NotNull] IPhaseResolver phaseResolver,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventsResolver eventsResolver)
        {
            ArgumentNullException.ThrowIfNull(phaseResolver);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventsResolver);

            _phaseResolver = phaseResolver;
            _eventEnqueuer = eventEnqueuer;
            _eventsResolver = eventsResolver;
        }

        public void Initialize()
        {
            Uninitialize();

            SubscribeToEvents();
        }

        public void Uninitialize()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            UnsubscribeFromEvents();

            _phaseResolver.OnEndIteration += HandleEndIteration;
        }

        private void UnsubscribeFromEvents()
        {
            _phaseResolver.OnEndIteration -= HandleEndIteration;
        }

        private void HandleEndIteration()
        {
            if (Resolving)
            {
                InvalidOperationException.Throw("Resolve is already in progress");
            }

            Resolving = true;

            ResolveNextEvent();
        }

        private void ResolveNextEvent()
        {
            if (!_eventEnqueuer.TryDequeue(out IEvent evt))
            {
                Resolving = false;

                return;
            }

            _eventsResolver.Resolve(evt, ResolveNextEvent);
        }
    }
}