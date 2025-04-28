using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.View.EventResolution.EventResolvers;

namespace Game.Gameplay.View.EventResolution
{
    public class EventResolverFactory : IEventResolverFactory
    {
        public IEventResolver<InstantiateEvent> GetInstantiate()
        {
            return new InstantiateEventResolver();
        }
    }
}