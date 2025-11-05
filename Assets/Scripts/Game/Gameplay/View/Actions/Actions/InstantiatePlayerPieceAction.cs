using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Player;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Actions.Actions
{
    public class InstantiatePlayerPieceAction : BaseInstantiatePieceAction
    {
        [NotNull] private readonly IPlayerPieceView _playerPieceView;

        public InstantiatePlayerPieceAction(
            [NotNull] IPiece piece,
            InstantiatePieceReason instantiatePieceReason,
            [NotNull] IPlayerPieceView playerPieceView) : base(piece, instantiatePieceReason)
        {
            ArgumentNullException.ThrowIfNull(playerPieceView);

            _playerPieceView = playerPieceView;
        }

        protected override GameObject InstantiatePiece(IPiece piece)
        {
            _playerPieceView.Instantiate(piece);

            GameObject instance = _playerPieceView.Instance;

            InvalidOperationException.ThrowIfNull(instance);

            return instance;
        }
    }
}