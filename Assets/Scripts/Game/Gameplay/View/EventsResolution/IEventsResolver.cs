using Game.Gameplay.EventEnqueueing.Events;

namespace Game.Gameplay.View.EventsResolution
{
    public interface IEventsResolver
    {
        void Resolve(IEvent evt);
    }
}