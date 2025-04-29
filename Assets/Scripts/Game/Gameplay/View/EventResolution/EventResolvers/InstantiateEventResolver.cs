using System;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Board.Pieces;
using Infrastructure.ModelViewViewModel;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public class InstantiateEventResolver : IEventResolver<InstantiateEvent>
    {
        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;
        [NotNull] private readonly IBoardViewController _boardViewController;

        public InstantiateEventResolver(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IBoardViewController boardViewController)
        {
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);
            ArgumentNullException.ThrowIfNull(boardViewController);

            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
            _boardViewController = boardViewController;
        }

        public void Resolve([NotNull] InstantiateEvent evt, Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(evt);

            IPieceViewDefinition pieceViewDefinition = _pieceViewDefinitionGetter.Get(evt.PieceType);

            GameObject instance =
                _boardViewController.Instantiate(
                    evt.Piece,
                    evt.SourceCoordinate,
                    pieceViewDefinition.Prefab
                );

            IDataSettable<IPiece> dataSettable = instance.GetComponent<IDataSettable<IPiece>>();
            IPieceViewEventNotifier pieceViewEventNotifier = instance.GetComponent<IPieceViewEventNotifier>();

            InvalidOperationException.ThrowIfNull(dataSettable);
            InvalidOperationException.ThrowIfNull(pieceViewEventNotifier);

            dataSettable.SetData(evt.Piece);
            pieceViewEventNotifier.OnInstantiated(evt.InstantiateReason, onComplete);
        }
    }
}