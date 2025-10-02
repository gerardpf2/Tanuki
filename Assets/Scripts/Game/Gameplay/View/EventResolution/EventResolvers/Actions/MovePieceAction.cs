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
        private readonly uint _id;
        private readonly int _rowOffset;
        private readonly int _columnOffset;
        private readonly MovePieceReason _movePieceReason;

        public MovePieceAction(
            [NotNull] IBoardView boardView,
            uint id,
            int rowOffset,
            int columnOffset,
            MovePieceReason movePieceReason)
        {
            ArgumentNullException.ThrowIfNull(boardView);

            _boardView = boardView;
            _id = id;
            _rowOffset = rowOffset;
            _columnOffset = columnOffset;
            _movePieceReason = movePieceReason;
        }

        public void Resolve(Action onComplete)
        {
            GameObject pieceInstance = _boardView.GetPieceInstance(_id);

            IPieceViewEventNotifier pieceViewEventNotifier = pieceInstance.GetComponent<IPieceViewEventNotifier>();

            InvalidOperationException.ThrowIfNull(pieceViewEventNotifier);

            // TODO: Tween

            pieceViewEventNotifier.OnStartMove(_movePieceReason);
            pieceViewEventNotifier.OnEndMove(OnComplete);

            return;

            void OnComplete()
            {
                _boardView.MovePiece(_id, _rowOffset, _columnOffset);

                onComplete?.Invoke();
            }
        }
    }
}