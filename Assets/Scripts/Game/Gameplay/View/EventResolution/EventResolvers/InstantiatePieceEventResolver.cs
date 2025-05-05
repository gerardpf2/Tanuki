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
    public class InstantiatePieceEventResolver : IEventResolver<InstantiatePieceEvent>
    {
        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;
        [NotNull] private readonly IBoardView _boardView;

        public InstantiatePieceEventResolver(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IBoardView boardView)
        {
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);
            ArgumentNullException.ThrowIfNull(boardView);

            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
            _boardView = boardView;
        }

        public void Resolve([NotNull] InstantiatePieceEvent evt, Action onComplete)
        {
            // TODO: Reuse code InstantiatePieceEventResolver and InstantiatePlayerPieceEventResolver

            ArgumentNullException.ThrowIfNull(evt);

            IPieceViewDefinition pieceViewDefinition = _pieceViewDefinitionGetter.Get(evt.PieceType);

            GameObject instance =
                _boardView.Instantiate(
                    evt.Piece,
                    evt.SourceCoordinate,
                    pieceViewDefinition.Prefab
                );

            IDataSettable<IPiece> dataSettable = instance.GetComponent<IDataSettable<IPiece>>();
            IPieceViewEventNotifier pieceViewEventNotifier = instance.GetComponent<IPieceViewEventNotifier>();

            InvalidOperationException.ThrowIfNull(dataSettable);
            InvalidOperationException.ThrowIfNull(pieceViewEventNotifier);

            dataSettable.SetData(evt.Piece);
            pieceViewEventNotifier.OnInstantiated(evt.InstantiatePieceReason, onComplete);
        }
    }
}