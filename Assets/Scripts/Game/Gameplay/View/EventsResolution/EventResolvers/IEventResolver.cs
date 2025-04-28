using Game.Gameplay.EventEnqueueing.Events;

namespace Game.Gameplay.View.EventsResolution.EventResolvers
{
    public interface IEventResolver<in T> where T : IEvent
    {
        void Resolve(T evt);
    }
}