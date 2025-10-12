using System;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Board.Pieces;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public class MovePieceAction : IAction
    {
        [NotNull] private readonly IBoardView _boardView;
        private readonly int _pieceId;
        private readonly int _rowOffset;
        private readonly int _columnOffset;
        private readonly MovePieceReason _movePieceReason;

        public MovePieceAction(
            [NotNull] IBoardView boardView,
            int pieceId,
            int rowOffset,
            int columnOffset,
            MovePieceReason movePieceReason)
        {
            ArgumentNullException.ThrowIfNull(boardView);

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
                // TODO: Tween / other depending on the reason (gravity, teleport, etc)

                OnMovementComplete();
            }

            void OnMovementComplete()
            {
                _boardView.MovePiece(_pieceId, _rowOffset, _columnOffset);

                pieceViewEventNotifier.OnEndMove(_movePieceReason, onComplete);
            }
        }
    }
}