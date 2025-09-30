using System;
using Game.Gameplay.EventEnqueueing.Events;

namespace Game.Gameplay.View.EventResolution
{
    public interface IEventsResolverSingle
    {
        void Resolve(IEvent evt, Action onComplete);
    }
}