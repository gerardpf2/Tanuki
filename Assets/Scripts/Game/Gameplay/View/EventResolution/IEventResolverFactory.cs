using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.View.EventResolution.EventResolvers;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolution
{
    public interface IEventResolverFactory
    {
        [NotNull]
        IEventResolver<InstantiateEvent> GetInstantiate();

        [NotNull]
        IEventResolver<InstantiatePlayerPieceEvent> GetInstantiatePlayerPiece();
    }
}