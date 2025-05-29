using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Game.Gameplay.View.Player;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public class DestroyPlayerPieceAction : BaseDestroyPieceAction
    {
        [NotNull] private readonly IPlayerView _playerView;

        public DestroyPlayerPieceAction(DestroyPieceReason destroyPieceReason, [NotNull] IPlayerView playerView) : base(destroyPieceReason)
        {
            ArgumentNullException.ThrowIfNull(playerView);

            _playerView = playerView;
        }

        protected override GameObject GetPieceInstance()
        {
            GameObject pieceInstance = _playerView.PieceInstance;

            InvalidOperationException.ThrowIfNull(pieceInstance);

            return pieceInstance;
        }

        protected override void DestroyPiece()
        {
            _playerView.DestroyPiece();
        }
    }
}