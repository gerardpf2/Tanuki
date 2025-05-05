using System;
using Game.Gameplay.EventEnqueueing.Events;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;

namespace Game.Gameplay.View.EventResolution
{
    public class EventsResolver : IEventsResolver
    {
        [NotNull] private readonly IEventResolverFactory _eventResolverFactory;

        public EventsResolver([NotNull] IEventResolverFactory eventResolverFactory)
        {
            ArgumentNullException.ThrowIfNull(eventResolverFactory);

            _eventResolverFactory = eventResolverFactory;
        }

        public void Resolve([NotNull] IEvent evt, Action onComplete)
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
                default:
                    ArgumentOutOfRangeException.Throw(evt);
                    return;
            }
        }
    }
}