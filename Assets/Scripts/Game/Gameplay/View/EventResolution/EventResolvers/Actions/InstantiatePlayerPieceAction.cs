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
        [NotNull] private readonly IPlayerView _playerView;

        public InstantiatePlayerPieceAction(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IPiece piece,
            InstantiatePieceReason instantiatePieceReason,
            [NotNull] IPlayerView playerView) : base(pieceViewDefinitionGetter, piece, instantiatePieceReason)
        {
            ArgumentNullException.ThrowIfNull(playerView);

            _playerView = playerView;
        }

        protected override GameObject Instantiate(IPiece piece, [NotNull] IPieceViewDefinition pieceViewDefinition)
        {
            ArgumentNullException.ThrowIfNull(pieceViewDefinition);

            _playerView.Instantiate(piece, pieceViewDefinition.Prefab);

            GameObject instance = _playerView.Instance;

            InvalidOperationException.ThrowIfNull(instance);

            return instance;
        }
    }
}