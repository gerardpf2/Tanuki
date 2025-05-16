using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.View.EventResolution.EventResolvers;
using Game.Gameplay.View.Player;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolution
{
    public class EventResolverFactory : IEventResolverFactory
    {
        // TODO: Reuse instead of new ¿?

        [NotNull] private readonly IActionFactory _actionFactory;
        [NotNull] private readonly IPlayerView _playerView;

        public EventResolverFactory([NotNull] IActionFactory actionFactory, [NotNull] IPlayerView playerView)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);
            ArgumentNullException.ThrowIfNull(playerView);

            _actionFactory = actionFactory;
            _playerView = playerView;
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
            return new LockPlayerPieceEventResolver(_playerView);
        }
    }
}