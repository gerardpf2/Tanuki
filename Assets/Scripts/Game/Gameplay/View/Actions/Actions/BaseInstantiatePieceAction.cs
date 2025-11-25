using System;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Pieces.EventNotifiers;
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

            IDataSettable<IPiece> dataSettable = pieceInstance.GetComponent<IDataSettable<IPiece>>();
            IPieceViewInstantiateEventNotifier pieceViewInstantiateEventNotifier = pieceInstance.GetComponent<IPieceViewInstantiateEventNotifier>();

            InvalidOperationException.ThrowIfNull(dataSettable);
            InvalidOperationException.ThrowIfNull(pieceViewInstantiateEventNotifier);

            dataSettable.SetData(_piece);
            pieceViewInstantiateEventNotifier.OnInstantiated(_instantiatePieceReason, onComplete);
        }

        [NotNull]
        protected abstract GameObject InstantiatePiece(IPiece piece);
    }
}