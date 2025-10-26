using System;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Board.Pieces;
using Infrastructure.ModelViewViewModel;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public abstract class BaseInstantiatePieceAction : IAction
    {
        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;
        [NotNull] private readonly IPiece _piece;
        private readonly InstantiatePieceReason _instantiatePieceReason;

        protected BaseInstantiatePieceAction(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IPiece piece,
            InstantiatePieceReason instantiatePieceReason)
        {
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);
            ArgumentNullException.ThrowIfNull(piece);

            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
            _piece = piece;
            _instantiatePieceReason = instantiatePieceReason;
        }

        public void Resolve(Action onComplete)
        {
            IPieceViewDefinition pieceViewDefinition = _pieceViewDefinitionGetter.Get(_piece.Type);

            GameObject pieceInstance = InstantiatePiece(_piece, pieceViewDefinition);

            IDataSettable<IPiece> dataSettable = pieceInstance.GetComponent<IDataSettable<IPiece>>();
            IPieceViewEventNotifier pieceViewEventNotifier = pieceInstance.GetComponent<IPieceViewEventNotifier>();

            InvalidOperationException.ThrowIfNull(dataSettable);
            InvalidOperationException.ThrowIfNull(pieceViewEventNotifier);

            dataSettable.SetData(_piece);
            pieceViewEventNotifier.OnInstantiated(_instantiatePieceReason, onComplete);
        }

        [NotNull]
        protected abstract GameObject InstantiatePiece(IPiece piece, IPieceViewDefinition pieceViewDefinition);
    }
}