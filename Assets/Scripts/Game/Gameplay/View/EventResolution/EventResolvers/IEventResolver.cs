using System;
using Game.Gameplay.Events.Events;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public interface IEventResolver<in T> where T : IEvent
    {
        void Resolve(T evt, Action onComplete);
    }
}