using System;
using System.Collections.Generic;
using Game.Common;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Pieces.EventNotifiers;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Actions.Actions
{
    public class DamagePieceAction : IAction
    {
        private readonly int _pieceId;
        private readonly IEnumerable<KeyValuePair<string, string>> _state;
        private readonly DamagePieceReason _damagePieceReason;
        private readonly Direction _direction;
        [NotNull] private readonly IBoardView _boardView;

        public DamagePieceAction(
            int pieceId,
            IEnumerable<KeyValuePair<string, string>> state,
            DamagePieceReason damagePieceReason,
            Direction direction,
            [NotNull] IBoardView boardView)
        {
            ArgumentNullException.ThrowIfNull(boardView);

            _pieceId = pieceId;
            _state = state;
            _damagePieceReason = damagePieceReason;
            _direction = direction;
            _boardView = boardView;
        }

        public void Resolve(Action onComplete)
        {
            IPiece piece = _boardView.GetPiece(_pieceId);

            piece.ProcessState(_state);

            GameObject pieceInstance = _boardView.GetPieceInstance(_pieceId);

            IPieceViewDamageEventNotifier pieceViewDamageEventNotifier = pieceInstance.GetComponent<IPieceViewDamageEventNotifier>();

            InvalidOperationException.ThrowIfNull(pieceViewDamageEventNotifier);

            pieceViewDamageEventNotifier.OnDamaged(_damagePieceReason, _direction, onComplete);
        }
    }
}