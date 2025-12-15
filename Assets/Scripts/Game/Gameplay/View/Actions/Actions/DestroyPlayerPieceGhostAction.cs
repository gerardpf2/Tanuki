using Game.Gameplay.Events.Reasons;
using Game.Gameplay.View.Player;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Actions.Actions
{
    public class DestroyPlayerPieceGhostAction : BaseDestroyPieceAction
    {
        [NotNull] private readonly IPlayerPieceGhostView _playerPieceGhostView;

        public DestroyPlayerPieceGhostAction(
            DestroyPieceReason destroyPieceReason,
            [NotNull] IPlayerPieceGhostView playerPieceGhostView) : base(destroyPieceReason)
        {
            ArgumentNullException.ThrowIfNull(playerPieceGhostView);

            _playerPieceGhostView = playerPieceGhostView;
        }

        protected override GameObject GetPieceInstance()
        {
            GameObject instance = _playerPieceGhostView.Instance;

            InvalidOperationException.ThrowIfNull(instance);

            return instance;
        }

        protected override void DestroyPiece()
        {
            _playerPieceGhostView.Destroy();
        }
    }
}