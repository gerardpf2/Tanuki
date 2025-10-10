using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Player;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public class InstantiatePlayerPieceAction : BaseInstantiatePieceAction
    {
        [NotNull] private readonly IPiecePlayerView _piecePlayerView;

        public InstantiatePlayerPieceAction(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IPiece piece,
            InstantiatePieceReason instantiatePieceReason,
            [NotNull] IPiecePlayerView piecePlayerView) : base(pieceViewDefinitionGetter, piece, instantiatePieceReason)
        {
            ArgumentNullException.ThrowIfNull(piecePlayerView);

            _piecePlayerView = piecePlayerView;
        }

        protected override GameObject InstantiatePiece(IPiece piece, [NotNull] IPieceViewDefinition pieceViewDefinition)
        {
            ArgumentNullException.ThrowIfNull(pieceViewDefinition);

            _piecePlayerView.Instantiate(piece, pieceViewDefinition.Prefab);

            GameObject instance = _piecePlayerView.Instance;

            InvalidOperationException.ThrowIfNull(instance);

            return instance;
        }
    }
}