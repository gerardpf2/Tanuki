using Game.Gameplay.Events.Events;
using JetBrains.Annotations;

namespace Game.Gameplay.Events
{
    public interface IEventEnqueuer
    {
        void Enqueue(IEvent evt);

        [ContractAnnotation("=> true, evt:notnull; => false, evt:null")]
        bool TryDequeue(out IEvent evt);
    }
}