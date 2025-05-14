using System;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Board.Pieces;
using Game.Gameplay.View.Player;
using Infrastructure.ModelViewViewModel;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public class InstantiatePlayerPieceEventResolver : IEventResolver<InstantiatePlayerPieceEvent>
    {
        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;
        [NotNull] private readonly IPlayerView _playerView;

        public InstantiatePlayerPieceEventResolver(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IPlayerView playerView)
        {
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);
            ArgumentNullException.ThrowIfNull(playerView);

            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
            _playerView = playerView;
        }

        public void Resolve([NotNull] InstantiatePlayerPieceEvent evt, Action onComplete)
        {
            // TODO: Reuse code InstantiatePieceEventResolver and InstantiatePlayerPieceEventResolver

            ArgumentNullException.ThrowIfNull(evt);

            IPieceViewDefinition pieceViewDefinition = _pieceViewDefinitionGetter.Get(evt.Piece.Type);

            GameObject instance = _playerView.Instantiate(evt.Piece, pieceViewDefinition.Prefab);

            IDataSettable<IPiece> dataSettable = instance.GetComponent<IDataSettable<IPiece>>();
            IPieceViewEventNotifier pieceViewEventNotifier = instance.GetComponent<IPieceViewEventNotifier>();

            InvalidOperationException.ThrowIfNull(dataSettable);
            InvalidOperationException.ThrowIfNull(pieceViewEventNotifier);

            dataSettable.SetData(evt.Piece);
            pieceViewEventNotifier.OnInstantiated(InstantiatePieceReason.Player, onComplete);
        }
    }
}