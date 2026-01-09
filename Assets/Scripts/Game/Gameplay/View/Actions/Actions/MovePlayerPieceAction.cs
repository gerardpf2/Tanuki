using Game.Gameplay.Board;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Animation.Movement;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Player;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Actions.Actions
{
    public class MovePlayerPieceAction : BaseMovePieceAction
    {
        [NotNull] private readonly IPlayerPieceView _playerPieceView;

        public MovePlayerPieceAction(
            [NotNull] IBoard board,
            [NotNull] IMovementHelper movementHelper,
            [NotNull] IBoardView boardView,
            int rowOffset,
            int columnOffset,
            MovePieceReason movePieceReason,
            [NotNull] IPlayerPieceView playerPieceView) : base(board, movementHelper, boardView, rowOffset, columnOffset, movePieceReason)
        {
            ArgumentNullException.ThrowIfNull(playerPieceView);

            _playerPieceView = playerPieceView;
        }

        protected override IPiece GetPiece()
        {
            IPiece piece = _playerPieceView.Piece;

            InvalidOperationException.ThrowIfNull(piece);

            return piece;
        }

        protected override Coordinate GetSourceCoordinate()
        {
            return _playerPieceView.Coordinate;
        }

        protected override GameObject GetPieceInstance()
        {
            GameObject instance = _playerPieceView.Instance;

            InvalidOperationException.ThrowIfNull(instance);

            return instance;
        }

        protected override void MovePiece(int _, int __) { }
    }
}