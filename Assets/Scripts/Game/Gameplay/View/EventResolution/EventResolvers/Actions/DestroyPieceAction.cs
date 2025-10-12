using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Header.Goals;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public class DestroyPieceAction : BaseDestroyPieceAction
    {
        private readonly int _pieceId;
        [NotNull] private readonly IBoardView _boardView;
        [NotNull] private readonly IGoalsView _goalsView;

        public DestroyPieceAction(
            DestroyPieceReason destroyPieceReason,
            int pieceId,
            [NotNull] IBoardView boardView,
            [NotNull] IGoalsView goalsView) : base(destroyPieceReason)
        {
            ArgumentNullException.ThrowIfNull(boardView);
            ArgumentNullException.ThrowIfNull(goalsView);

            _pieceId = pieceId;
            _boardView = boardView;
            _goalsView = goalsView;
        }

        protected override GameObject GetPieceInstance()
        {
            return _boardView.GetPieceInstance(_pieceId);
        }

        protected override void DestroyPiece()
        {
            IPiece piece = _boardView.GetPiece(_pieceId);

            _boardView.DestroyPiece(_pieceId);
            _goalsView.TryIncreaseCurrentAmount(piece.Type);
        }
    }
}