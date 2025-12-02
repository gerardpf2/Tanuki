using System;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Pieces.EventNotifiers;
using Infrastructure.ModelViewViewModel;
using Infrastructure.Unity;
using Infrastructure.Unity.Utils;
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
            ICoroutineRunner coroutineRunner = pieceInstance.GetComponent<ICoroutineRunner>();
            IPieceViewInstantiateEventNotifier pieceViewInstantiateEventNotifier = pieceInstance.GetComponent<IPieceViewInstantiateEventNotifier>();

            InvalidOperationException.ThrowIfNull(dataSettable);
            InvalidOperationException.ThrowIfNull(coroutineRunner);
            InvalidOperationException.ThrowIfNull(pieceViewInstantiateEventNotifier);

            dataSettable.SetData(_piece);

            /*
             *
             * Not ideal, but it is needed to wait for end of frame so all bindings, especially the ones related to the
             * animator, are ready. Otherwise, on complete callback may not be called
             *
             */

            coroutineRunner.Run(CoroutineUtils.GetWaitForEndOfFrame(NotifyInstantiated));

            return;

            void NotifyInstantiated()
            {
                pieceViewInstantiateEventNotifier.OnInstantiated(_instantiatePieceReason, onComplete);
            }
        }

        [NotNull]
        protected abstract GameObject InstantiatePiece(IPiece piece);
    }
}