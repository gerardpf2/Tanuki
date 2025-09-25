using System;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Board.Pieces;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public class DamagePieceAction : IAction
    {
        private readonly IPiece _piece;
        [NotNull] private readonly IBoardView _boardView;

        public DamagePieceAction(IPiece piece, [NotNull] IBoardView boardView)
        {
            ArgumentNullException.ThrowIfNull(boardView);

            _piece = piece;
            _boardView = boardView;
        }

        public void Resolve(Action onComplete)
        {
            GameObject pieceInstance = _boardView.GetPieceInstance(_piece);

            IPieceViewEventNotifier pieceViewEventNotifier = pieceInstance.GetComponent<IPieceViewEventNotifier>();

            InvalidOperationException.ThrowIfNull(pieceViewEventNotifier);

            pieceViewEventNotifier.OnDamaged(onComplete);
        }
    }
}