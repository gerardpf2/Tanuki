using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.View.EventsResolution.EventResolvers;

namespace Game.Gameplay.View.EventsResolution
{
    public class EventResolverFactory : IEventResolverFactory
    {
        public IEventResolver<InstantiateEvent> GetInstantiate()
        {
            return new InstantiateEventResolver();
        }
    }
}