using Game.Gameplay.EventEnqueueing.Events;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

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

        public void Resolve([NotNull] IEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            switch (evt)
            {
                case InstantiateEvent instantiateEvent:
                    _eventResolverFactory.GetInstantiate().Resolve(instantiateEvent);
                    break;
                default:
                    ArgumentOutOfRangeException.Throw(evt);
                    return;
            }
        }
    }
}