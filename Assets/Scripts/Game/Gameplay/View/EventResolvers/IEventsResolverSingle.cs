using System;
using Game.Gameplay.Events.Events;

namespace Game.Gameplay.View.EventResolvers
{
    public interface IEventsResolverSingle
    {
        void Resolve(IEvent evt, Action onComplete);
    }
}