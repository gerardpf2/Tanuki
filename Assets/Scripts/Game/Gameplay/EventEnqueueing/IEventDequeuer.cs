using System;
using Game.Gameplay.EventEnqueueing.Events;
using JetBrains.Annotations;

namespace Game.Gameplay.EventEnqueueing
{
    public interface IEventDequeuer
    {
        event Action OnEventToDequeue;

        [ContractAnnotation("=> true, evt:notnull; => false, evt:null")]
        bool TryDequeue(out IEvent evt);
    }
}