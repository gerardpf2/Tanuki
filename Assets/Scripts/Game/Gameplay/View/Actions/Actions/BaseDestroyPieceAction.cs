using System;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.View.Pieces.EventNotifiers;
using JetBrains.Annotations;
using UnityEngine;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Actions.Actions
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

            IPieceViewInstantiateEventNotifier pieceViewInstantiateEventNotifier = pieceInstance.GetComponent<IPieceViewInstantiateEventNotifier>();

            InvalidOperationException.ThrowIfNull(pieceViewInstantiateEventNotifier);

            pieceViewInstantiateEventNotifier.OnDestroyed(_destroyPieceReason, OnComplete);

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