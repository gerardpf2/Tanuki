using System;
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
        [NotNull] private readonly IPlayerView _playerView;

        public DestroyPlayerPieceAction([NotNull] IPlayerView playerView)
        {
            ArgumentNullException.ThrowIfNull(playerView);

            _playerView = playerView;
        }

        public void Resolve(Action onComplete)
        {
            GameObject pieceInstance = _playerView.PieceInstance;

            InvalidOperationException.ThrowIfNull(pieceInstance);

            IPieceViewEventNotifier pieceViewEventNotifier = pieceInstance.GetComponent<IPieceViewEventNotifier>();

            InvalidOperationException.ThrowIfNull(pieceViewEventNotifier);

            pieceViewEventNotifier.OnDestroyed(OnComplete);

            return;

            void OnComplete()
            {
                _playerView.DestroyPiece();

                onComplete?.Invoke();
            }
        }
    }
}