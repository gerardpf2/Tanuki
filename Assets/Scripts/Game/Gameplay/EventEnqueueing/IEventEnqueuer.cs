using Game.Gameplay.EventEnqueueing.Events;

namespace Game.Gameplay.EventEnqueueing
{
    public interface IEventEnqueuer
    {
        void Enqueue(IEvent evt);
    }
}