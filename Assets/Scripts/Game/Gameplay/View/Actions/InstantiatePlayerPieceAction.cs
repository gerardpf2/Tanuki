using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Pieces;
using Game.Gameplay.View.Player;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Actions
{
    public class InstantiatePlayerPieceAction : BaseInstantiatePieceAction
    {
        [NotNull] private readonly IPlayerPieceView _playerPieceView;

        public InstantiatePlayerPieceAction(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IPiece piece,
            InstantiatePieceReason instantiatePieceReason,
            [NotNull] IPlayerPieceView playerPieceView) : base(pieceViewDefinitionGetter, piece, instantiatePieceReason)
        {
            ArgumentNullException.ThrowIfNull(playerPieceView);

            _playerPieceView = playerPieceView;
        }

        protected override GameObject InstantiatePiece(IPiece piece, [NotNull] IPieceViewDefinition pieceViewDefinition)
        {
            ArgumentNullException.ThrowIfNull(pieceViewDefinition);

            _playerPieceView.Instantiate(piece, pieceViewDefinition.Prefab);

            GameObject instance = _playerPieceView.Instance;

            InvalidOperationException.ThrowIfNull(instance);

            return instance;
        }
    }
}