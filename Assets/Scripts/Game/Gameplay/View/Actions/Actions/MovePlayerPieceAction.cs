using Game.Gameplay.Events.Reasons;
using Game.Gameplay.View.Animation.Movement;
using Game.Gameplay.View.Player;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Actions.Actions
{
    public class MovePlayerPieceAction : BaseMovePieceAction
    {
        [NotNull] private readonly IPlayerPieceView _playerPieceView;

        public MovePlayerPieceAction(
            [NotNull] IMovementHelper movementHelper,
            int rowOffset,
            int columnOffset,
            MovePieceReason movePieceReason,
            [NotNull] IPlayerPieceView playerPieceView) : base(movementHelper, rowOffset, columnOffset, movePieceReason)
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

        protected override void MovePiece(int _, int __) { }
    }
}