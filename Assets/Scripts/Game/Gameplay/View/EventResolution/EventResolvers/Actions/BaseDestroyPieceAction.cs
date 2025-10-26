using System;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.View.Board.Pieces;
using JetBrains.Annotations;
using UnityEngine;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public abstract class BaseDestroyPieceAction : IAction
    {
        private readonly DestroyPieceReason _destroyPieceReason;

        protected BaseDestroyPieceAction(DestroyPieceReason destroyPieceReason)
        {
            _destroyPieceReason = destroyPieceReason;
        }

        public void Resolve(Action onComplete)
        {
            GameObject pieceInstance = GetPieceInstance();

            IPieceViewEventNotifier pieceViewEventNotifier = pieceInstance.GetComponent<IPieceViewEventNotifier>();

            InvalidOperationException.ThrowIfNull(pieceViewEventNotifier);

            pieceViewEventNotifier.OnDestroyed(_destroyPieceReason, OnComplete);

            return;

            void OnComplete()
            {
                DestroyPiece();

                onComplete?.Invoke();
            }
        }

        [NotNull]
        protected abstract GameObject GetPieceInstance();

        protected abstract void DestroyPiece();
    }
}