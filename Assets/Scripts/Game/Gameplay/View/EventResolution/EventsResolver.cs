using System;
using Game.Gameplay.EventEnqueueing.Events;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.EventResolution
{
    public class EventsResolver : IEventsResolver
    {
        [NotNull] private readonly IEventResolverFactory _eventResolverFactory;

        public bool Resolving { get; private set; }

        public EventsResolver([NotNull] IEventResolverFactory eventResolverFactory)
        {
            ArgumentNullException.ThrowIfNull(eventResolverFactory);

            _eventResolverFactory = eventResolverFactory;
        }

        public void Resolve([NotNull] IEvent evt, Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(evt);

            if (Resolving)
            {
                InvalidOperationException.Throw("Resolve is already in progress");
            }

            Resolving = true;

            switch (evt)
            {
                case InstantiatePieceEvent instantiateEvent:
                    _eventResolverFactory.GetInstantiatePieceEventResolver().Resolve(instantiateEvent, OnComplete);
                    break;
                case InstantiatePlayerPieceEvent instantiatePlayerPieceEvent:
                    _eventResolverFactory.GetInstantiatePlayerPieceEventResolver().Resolve(instantiatePlayerPieceEvent, OnComplete);
                    break;
                case LockPlayerPieceEvent lockPlayerPieceEvent:
                    _eventResolverFactory.GetLockPlayerPieceEventResolver().Resolve(lockPlayerPieceEvent, OnComplete);
                    break;
                case DamagePieceEvent damagePieceEvent:
                    _eventResolverFactory.GetDamagePieceEventResolver().Resolve(damagePieceEvent, OnComplete);
                    break;
                case DestroyPieceEvent destroyPieceEvent:
                    _eventResolverFactory.GetDestroyPieceEventResolver().Resolve(destroyPieceEvent, OnComplete);
                    break;
                case MovePieceEvent movePieceEvent:
                    _eventResolverFactory.GetMovePieceEventResolver().Resolve(movePieceEvent, OnComplete);
                    break;
                default:
                    ArgumentOutOfRangeException.Throw(evt);
                    return;
            }

            return;

            void OnComplete()
            {
                Resolving = false;

                onComplete?.Invoke();
            }
        }
    }
}