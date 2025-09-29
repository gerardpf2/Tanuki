using System;
using Game.Gameplay.EventEnqueueing;
using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.PhaseResolution;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.EventResolution
{
    public class EventListener : IEventListener
    {
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IPhaseResolver _phaseResolver;
        [NotNull] private readonly IEventResolverFactory _eventResolverFactory;

        public bool Resolving { get; private set; }

        public EventListener(
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IPhaseResolver phaseResolver,
            [NotNull] IEventResolverFactory eventResolverFactory)
        {
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(phaseResolver);
            ArgumentNullException.ThrowIfNull(eventResolverFactory);

            _eventEnqueuer = eventEnqueuer;
            _phaseResolver = phaseResolver;
            _eventResolverFactory = eventResolverFactory;
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

            ResolveNext();
        }

        private void ResolveNext()
        {
            if (!_eventEnqueuer.TryDequeue(out IEvent evt))
            {
                Resolving = false;

                return;
            }

            Resolve(evt, ResolveNext);
        }

        private void Resolve([NotNull] IEvent evt, Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(evt);

            switch (evt)
            {
                case InstantiatePieceEvent instantiateEvent:
                    _eventResolverFactory.GetInstantiatePieceEventResolver().Resolve(instantiateEvent, onComplete);
                    break;
                case InstantiatePlayerPieceEvent instantiatePlayerPieceEvent:
                    _eventResolverFactory.GetInstantiatePlayerPieceEventResolver().Resolve(instantiatePlayerPieceEvent, onComplete);
                    break;
                case LockPlayerPieceEvent lockPlayerPieceEvent:
                    _eventResolverFactory.GetLockPlayerPieceEventResolver().Resolve(lockPlayerPieceEvent, onComplete);
                    break;
                case DamagePieceEvent damagePieceEvent:
                    _eventResolverFactory.GetDamagePieceEventResolver().Resolve(damagePieceEvent, onComplete);
                    break;
                case DestroyPieceEvent destroyPieceEvent:
                    _eventResolverFactory.GetDestroyPieceEventResolver().Resolve(destroyPieceEvent, onComplete);
                    break;
                case MovePieceEvent movePieceEvent:
                    _eventResolverFactory.GetMovePieceEventResolver().Resolve(movePieceEvent, onComplete);
                    break;
                default:
                    ArgumentOutOfRangeException.Throw(evt);
                    return;
            }
        }
    }
}