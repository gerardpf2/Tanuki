using System;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Board.Pieces;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public class DestroyPieceAction : IAction
    {
        private readonly IPiece _piece;
        private readonly DestroyPieceReason _destroyPieceReason;
        [NotNull] private readonly IReadonlyBoardView _boardView;

        public DestroyPieceAction(
            IPiece piece,
            DestroyPieceReason destroyPieceReason,
            [NotNull] IReadonlyBoardView boardView)
        {
            ArgumentNullException.ThrowIfNull(boardView);

            _piece = piece;
            _destroyPieceReason = destroyPieceReason;
            _boardView = boardView;
        }

        public void Resolve(Action onComplete)
        {
            GameObject pieceInstance = _boardView.GetPieceInstance(_piece);

            IPieceViewEventNotifier pieceViewEventNotifier = pieceInstance.GetComponent<IPieceViewEventNotifier>();

            InvalidOperationException.ThrowIfNull(pieceViewEventNotifier);

            pieceViewEventNotifier.OnDestroyed(_destroyPieceReason, OnComplete);

            return;

            void OnComplete()
            {
                // TODO
                // _boardView.DestroyPiece(_piece);

                onComplete?.Invoke();
            }
        }
    }
}