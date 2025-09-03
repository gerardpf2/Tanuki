using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.View.EventResolution.EventResolvers;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolution
{
    public class EventResolverFactory : IEventResolverFactory
    {
        [NotNull] private readonly IActionFactory _actionFactory;

        public EventResolverFactory([NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);

            _actionFactory = actionFactory;
        }

        public IEventResolver<InstantiatePieceEvent> GetInstantiatePieceEventResolver()
        {
            return new InstantiatePieceEventResolver(_actionFactory);
        }

        public IEventResolver<InstantiatePlayerPieceEvent> GetInstantiatePlayerPieceEventResolver()
        {
            return new InstantiatePlayerPieceEventResolver(_actionFactory);
        }

        public IEventResolver<LockPlayerPieceEvent> GetLockPlayerPieceEventResolver()
        {
            return new LockPlayerPieceEventResolver(_actionFactory);
        }

        public IEventResolver<DamagePieceEvent> GetDamagePieceEventResolver()
        {
            return new DamagePieceEventResolver(_actionFactory);
        }
    }
}