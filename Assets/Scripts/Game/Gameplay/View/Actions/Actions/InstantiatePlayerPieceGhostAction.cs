using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Player;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Actions.Actions
{
    public class InstantiatePlayerPieceGhostAction : BaseInstantiatePieceAction
    {
        [NotNull] private readonly IPlayerPieceGhostView _playerPieceGhostView;

        public InstantiatePlayerPieceGhostAction(
            [NotNull] IPiece piece,
            InstantiatePieceReason instantiatePieceReason,
            [NotNull] IPlayerPieceGhostView playerPieceGhostView) : base(piece, instantiatePieceReason)
        {
            ArgumentNullException.ThrowIfNull(playerPieceGhostView);

            _playerPieceGhostView = playerPieceGhostView;
        }

        protected override GameObject InstantiatePiece([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            _playerPieceGhostView.Instantiate(piece.Type);

            GameObject instance = _playerPieceGhostView.Instance;

            InvalidOperationException.ThrowIfNull(instance);

            return instance;
        }
    }
}