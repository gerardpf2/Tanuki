using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Game.Gameplay.View.Animation.Movement;
using Game.Gameplay.View.Board;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public class MovePieceAction : BaseMovePieceAction
    {
        [NotNull] private readonly IBoardView _boardView;
        private readonly int _pieceId;

        public MovePieceAction(
            [NotNull] IMovementHelper movementHelper,
            int rowOffset,
            int columnOffset,
            MovePieceReason movePieceReason,
            [NotNull] IBoardView boardView,
            int pieceId) : base(movementHelper, rowOffset, columnOffset, movePieceReason)
        {
            ArgumentNullException.ThrowIfNull(boardView);

            _boardView = boardView;
            _pieceId = pieceId;
        }

        protected override GameObject GetPieceInstance()
        {
            return _boardView.GetPieceInstance(_pieceId);
        }

        protected override void MovePiece(int rowOffset, int columnOffset)
        {
            _boardView.MovePiece(_pieceId, rowOffset, columnOffset);
        }
    }
}