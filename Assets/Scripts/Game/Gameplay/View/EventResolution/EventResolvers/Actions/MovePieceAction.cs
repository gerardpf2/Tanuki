using System;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Game.Gameplay.View.Animation.Movement;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Board.Pieces;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public class MovePieceAction : IAction
    {
        [NotNull] private readonly IMovementHelper _movementHelper;
        [NotNull] private readonly IBoardView _boardView;
        private readonly int _pieceId;
        private readonly int _rowOffset;
        private readonly int _columnOffset;
        private readonly MovePieceReason _movePieceReason;

        public MovePieceAction(
            [NotNull] IMovementHelper movementHelper,
            [NotNull] IBoardView boardView,
            int pieceId,
            int rowOffset,
            int columnOffset,
            MovePieceReason movePieceReason)
        {
            ArgumentNullException.ThrowIfNull(movementHelper);
            ArgumentNullException.ThrowIfNull(boardView);

            _movementHelper = movementHelper;
            _boardView = boardView;
            _pieceId = pieceId;
            _rowOffset = rowOffset;
            _columnOffset = columnOffset;
            _movePieceReason = movePieceReason;
        }

        public void Resolve(Action onComplete)
        {
            GameObject pieceInstance = _boardView.GetPieceInstance(_pieceId);

            IPieceViewEventNotifier pieceViewEventNotifier = pieceInstance.GetComponent<IPieceViewEventNotifier>();

            InvalidOperationException.ThrowIfNull(pieceViewEventNotifier);

            pieceViewEventNotifier.OnStartMove(_movePieceReason, OnStartMoveComplete);

            return;

            void OnStartMoveComplete()
            {
                DoMove(pieceInstance.transform, OnMovementComplete);
            }

            void OnMovementComplete()
            {
                _boardView.MovePiece(_pieceId, _rowOffset, _columnOffset);

                pieceViewEventNotifier.OnEndMove(_movePieceReason, onComplete);
            }
        }

        private void DoMove(Transform transform, Action onComplete)
        {
            switch (_movePieceReason)
            {
                case MovePieceReason.Gravity:
                    _movementHelper.DoGravityMovement(transform, _rowOffset, _columnOffset, onComplete);
                    break;
                default:
                    ArgumentOutOfRangeException.Throw(_movePieceReason);
                    return;
            }
        }
    }
}