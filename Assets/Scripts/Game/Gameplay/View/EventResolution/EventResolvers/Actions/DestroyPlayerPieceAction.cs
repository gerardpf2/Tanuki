using System;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Game.Gameplay.View.Board.Pieces;
using Game.Gameplay.View.Player;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public class DestroyPlayerPieceAction : IAction
    {
        private readonly DestroyPieceReason _destroyPieceReason;
        [NotNull] private readonly IPlayerView _playerView;

        public DestroyPlayerPieceAction(DestroyPieceReason destroyPieceReason, [NotNull] IPlayerView playerView)
        {
            ArgumentNullException.ThrowIfNull(playerView);

            _destroyPieceReason = destroyPieceReason;
            _playerView = playerView;
        }

        public void Resolve(Action onComplete)
        {
            GameObject pieceInstance = _playerView.PieceInstance;

            InvalidOperationException.ThrowIfNull(pieceInstance);

            IPieceViewEventNotifier pieceViewEventNotifier = pieceInstance.GetComponent<IPieceViewEventNotifier>();

            InvalidOperationException.ThrowIfNull(pieceViewEventNotifier);

            pieceViewEventNotifier.OnDestroyed(_destroyPieceReason, OnComplete);

            return;

            void OnComplete()
            {
                _playerView.DestroyPiece();

                onComplete?.Invoke();
            }
        }
    }
}