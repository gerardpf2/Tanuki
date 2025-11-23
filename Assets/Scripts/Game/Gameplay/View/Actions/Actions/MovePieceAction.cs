using Game.Gameplay.Board;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Animation.Movement;
using Game.Gameplay.View.Board;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Actions.Actions
{
    public class MovePieceAction : BaseMovePieceAction
    {
        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly IBoardView _boardView;
        private readonly int _pieceId;

        public MovePieceAction(
            [NotNull] IBoard board,
            [NotNull] IMovementHelper movementHelper,
            int rowOffset,
            int columnOffset,
            MovePieceReason movePieceReason,
            [NotNull] IBoardView boardView,
            int pieceId) : base(board, movementHelper, rowOffset, columnOffset, movePieceReason)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(boardView);

            _board = board;
            _boardView = boardView;
            _pieceId = pieceId;
        }

        protected override GameObject GetPieceInstance()
        {
            return _boardView.GetPieceInstance(_pieceId);
        }

        protected override void MovePiece(int rowOffset, int columnOffset)
        {
            _board.MovePiece(_pieceId, rowOffset, columnOffset);
        }

        protected override IPiece GetPiece()
        {
            return _board.GetPiece(_pieceId);
        }

        protected override Coordinate GetSourceCoordinate()
        {
            return _board.GetSourceCoordinate(_pieceId);
        }
    }
}