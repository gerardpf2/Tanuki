using Game.Common;
using Game.Gameplay.Events;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Phases;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolution
{
    public class EventsResolver : IEventsResolver
    {
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IPhaseResolver _phaseResolver;
        [NotNull] private readonly IEventsResolverSingle _eventsResolverSingle;

        private InitializedLabel _initializedLabel;

        public bool Resolving { get; private set; }

        public EventsResolver(
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IPhaseResolver phaseResolver,
            [NotNull] IEventsResolverSingle eventsResolverSingle)
        {
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(phaseResolver);
            ArgumentNullException.ThrowIfNull(eventsResolverSingle);

            _eventEnqueuer = eventEnqueuer;
            _phaseResolver = phaseResolver;
            _eventsResolverSingle = eventsResolverSingle;
        }

        public void Initialize()
        {
            _initializedLabel.SetInitialized();

            SubscribeToEvents();
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            if (Resolving)
            {
                InvalidOperationException.Throw("Resolve is still in progress");
            }

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

            ResolveNext();
        }

        private void ResolveNext()
        {
            if (!_eventEnqueuer.TryDequeue(out IEvent evt))
            {
                Resolving = false;

                return;
            }

            _eventsResolverSingle.Resolve(evt, ResolveNext);
        }
    }
}