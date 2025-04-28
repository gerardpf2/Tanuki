using Game.Gameplay.EventEnqueueing.Events;

namespace Game.Gameplay.View.EventResolution
{
    public interface IEventsResolver
    {
        void Resolve(IEvent evt);
    }
}