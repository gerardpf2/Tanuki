using System;
using Game.Gameplay.Board;
using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.View.Player;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public class LockPlayerPieceEventResolver : IEventResolver<LockPlayerPieceEvent>
    {
        [NotNull] private readonly IPlayerView _playerView;

        public LockPlayerPieceEventResolver([NotNull] IPlayerView playerView)
        {
            ArgumentNullException.ThrowIfNull(playerView);

            _playerView = playerView;
        }

        public void Resolve([NotNull] LockPlayerPieceEvent evt, Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(evt);

            // TODO
            // 1) Move to board position
            // 2) Despawn player
            // 3) Spawn board

            Coordinate start = _playerView.Coordinate;
        }
    }
}