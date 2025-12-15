using System;
using Game.Gameplay.Events.Events;

namespace Game.Gameplay.View.EventResolvers.EventResolvers
{
    public interface IEventResolver<in TEvent> where TEvent : IEvent
    {
        void Resolve(TEvent evt, Action onComplete);
    }
}