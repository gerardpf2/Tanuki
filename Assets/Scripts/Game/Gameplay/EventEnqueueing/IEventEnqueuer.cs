using Game.Gameplay.EventEnqueueing.Events;
using JetBrains.Annotations;

namespace Game.Gameplay.EventEnqueueing
{
    public interface IEventEnqueuer
    {
        void Enqueue(IEvent evt);

        [ContractAnnotation("=> true, evt:notnull; => false, evt:null")]
        bool TryDequeue(out IEvent evt);
    }
}