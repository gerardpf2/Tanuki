using Game.Gameplay.Events.Reasons;
using Game.Gameplay.View.Player;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Actions
{
    public class DestroyPlayerPieceAction : BaseDestroyPieceAction
    {
        [NotNull] private readonly IPlayerPieceView _playerPieceView;

        public DestroyPlayerPieceAction(DestroyPieceReason destroyPieceReason, [NotNull] IPlayerPieceView playerPieceView) : base(destroyPieceReason)
        {
            ArgumentNullException.ThrowIfNull(playerPieceView);

            _playerPieceView = playerPieceView;
        }

        protected override GameObject GetPieceInstance()
        {
            GameObject instance = _playerPieceView.Instance;

            InvalidOperationException.ThrowIfNull(instance);

            return instance;
        }

        protected override void DestroyPiece()
        {
            _playerPieceView.Destroy();
        }
    }
}