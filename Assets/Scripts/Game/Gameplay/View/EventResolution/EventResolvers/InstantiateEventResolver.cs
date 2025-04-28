using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.Model.Board.Pieces;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Board.Pieces;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public class InstantiateEventResolver : IEventResolver<InstantiateEvent>
    {
        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;
        [NotNull] private readonly IBoardView _boardView;

        public InstantiateEventResolver(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IBoardView boardView)
        {
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);
            ArgumentNullException.ThrowIfNull(boardView);

            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
            _boardView = boardView;
        }

        public void Resolve([NotNull] InstantiateEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            IPieceViewDefinition pieceViewDefinition = _pieceViewDefinitionGetter.Get(evt.PieceType);
            GameObject instance = Object.Instantiate(pieceViewDefinition.Prefab);

            _boardView.Add(evt.Piece, evt.SourceCoordinate, instance);

            IDataSettable<IPiece> dataSettable = instance.GetComponent<IDataSettable<IPiece>>();
            IPieceViewEventNotifier pieceViewEventNotifier = instance.GetComponent<IPieceViewEventNotifier>();

            InvalidOperationException.ThrowIfNull(dataSettable);
            InvalidOperationException.ThrowIfNull(pieceViewEventNotifier);

            dataSettable.SetData(evt.Piece);
            pieceViewEventNotifier.OnInstantiated();
        }
    }
}