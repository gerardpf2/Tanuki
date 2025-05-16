using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.EventResolution.EventResolvers;
using Game.Gameplay.View.Player;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolution
{
    public class EventResolverFactory : IEventResolverFactory
    {
        // TODO: Reuse instead of new ¿?

        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;
        [NotNull] private readonly IBoardView _boardView;
        [NotNull] private readonly IPlayerView _playerView;

        public EventResolverFactory(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IBoardView boardView,
            [NotNull] IPlayerView playerView)
        {
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);
            ArgumentNullException.ThrowIfNull(boardView);
            ArgumentNullException.ThrowIfNull(playerView);

            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
            _boardView = boardView;
            _playerView = playerView;
        }

        public IEventResolver<InstantiatePieceEvent> GetInstantiatePieceEventResolver()
        {
            return new InstantiatePieceEventResolver(_pieceViewDefinitionGetter, _boardView);
        }

        public IEventResolver<InstantiatePlayerPieceEvent> GetInstantiatePlayerPieceEventResolver()
        {
            return new InstantiatePlayerPieceEventResolver(_pieceViewDefinitionGetter, _playerView);
        }

        public IEventResolver<LockPlayerPieceEvent> GetLockPlayerPieceEventResolver()
        {
            return new LockPlayerPieceEventResolver(_playerView);
        }
    }
}