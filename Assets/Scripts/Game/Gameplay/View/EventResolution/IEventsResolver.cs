using System;
using Game.Gameplay.EventEnqueueing.Events;

namespace Game.Gameplay.View.EventResolution
{
    public interface IEventsResolver
    {
        bool Resolving { get; }

        void Resolve(IEvent evt, Action onComplete);
    }
}