using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.View.EventsResolution.EventResolvers;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventsResolution
{
    public interface IEventResolverFactory
    {
        [NotNull]
        IEventResolver<InstantiateEvent> GetInstantiate();
    }
}