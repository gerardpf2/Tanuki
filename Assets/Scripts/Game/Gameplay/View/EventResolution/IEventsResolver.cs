using System;
using Game.Gameplay.EventEnqueueing.Events;

namespace Game.Gameplay.View.EventResolution
{
    public interface IEventsResolver : IReadonlyEventsResolver
    {
        void Resolve(IEvent evt, Action onComplete);
    }
}