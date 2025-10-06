using System;
using System.Collections.Generic;
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
        private readonly int _id;
        private readonly IEnumerable<KeyValuePair<string, string>> _state;
        [NotNull] private readonly IBoardView _boardView;

        public DamagePieceAction(
            int id,
            IEnumerable<KeyValuePair<string, string>> state,
            [NotNull] IBoardView boardView)
        {
            ArgumentNullException.ThrowIfNull(boardView);

            _id = id;
            _state = state;
            _boardView = boardView;
        }

        public void Resolve(Action onComplete)
        {
            IPiece piece = _boardView.GetPiece(_id);

            piece.ProcessState(_state);

            GameObject pieceInstance = _boardView.GetPieceInstance(_id);

            IPieceViewEventNotifier pieceViewEventNotifier = pieceInstance.GetComponent<IPieceViewEventNotifier>();

            InvalidOperationException.ThrowIfNull(pieceViewEventNotifier);

            pieceViewEventNotifier.OnDamaged(onComplete);
        }
    }
}