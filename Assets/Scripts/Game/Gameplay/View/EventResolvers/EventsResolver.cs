using System;
using Game.Common;
using Game.Gameplay.Events;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Phases;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.EventResolvers
{
    public class EventsResolver : IEventsResolver
    {
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IPhaseResolver _phaseResolver;
        [NotNull] private readonly IEventsResolverSingle _eventsResolverSingle;

        private InitializedLabel _initializedLabel;
        private bool _resolving;

        public event Action OnResolveBegin;
        public event Action OnResolveEnd;

        public bool Resolving
        {
            get => _resolving;
            private set
            {
                if (Resolving == value)
                {
                    return;
                }

                _resolving = value;

                if (Resolving)
                {
                    OnResolveBegin?.Invoke();
                }
                else
                {
                    OnResolveEnd?.Invoke();
                }
            }
        }

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