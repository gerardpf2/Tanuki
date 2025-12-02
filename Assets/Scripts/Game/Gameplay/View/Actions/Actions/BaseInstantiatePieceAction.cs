using System;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Pieces.EventNotifiers;
using Game.Gameplay.View.Pieces.Pieces;
using Infrastructure.ModelViewViewModel;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Actions.Actions
{
    public abstract class BaseInstantiatePieceAction : IAction
    {
        [NotNull] private readonly IPiece _piece;
        private readonly InstantiatePieceReason _instantiatePieceReason;

        protected BaseInstantiatePieceAction([NotNull] IPiece piece, InstantiatePieceReason instantiatePieceReason)
        {
            ArgumentNullException.ThrowIfNull(piece);

            _piece = piece;
            _instantiatePieceReason = instantiatePieceReason;
        }

        public void Resolve(Action onComplete)
        {
            GameObject pieceInstance = InstantiatePiece(_piece);

            IDataSettable<PieceViewData> dataSettable = pieceInstance.GetComponent<IDataSettable<PieceViewData>>();

            InvalidOperationException.ThrowIfNull(dataSettable);

            PieceViewData pieceViewData = new(_piece, HandleReady);

            dataSettable.SetData(pieceViewData);

            return;

            void HandleReady()
            {
                IPieceViewInstantiateEventNotifier pieceViewInstantiateEventNotifier = pieceInstance.GetComponent<IPieceViewInstantiateEventNotifier>();

                InvalidOperationException.ThrowIfNull(pieceViewInstantiateEventNotifier);

                pieceViewInstantiateEventNotifier.OnInstantiated(_instantiatePieceReason, onComplete);
            }
        }

        [NotNull]
        protected abstract GameObject InstantiatePiece(IPiece piece);
    }
}