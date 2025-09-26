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
        [NotNull] private readonly IPiece _piece;
        [NotNull] private readonly IBoardView _boardView;
        [NotNull] private readonly IGoalsView _goalsView;

        public DestroyPieceAction(
            DestroyPieceReason destroyPieceReason,
            [NotNull] IPiece piece,
            [NotNull] IBoardView boardView,
            [NotNull] IGoalsView goalsView) : base(destroyPieceReason)
        {
            ArgumentNullException.ThrowIfNull(piece);
            ArgumentNullException.ThrowIfNull(boardView);
            ArgumentNullException.ThrowIfNull(goalsView);

            _piece = piece;
            _boardView = boardView;
            _goalsView = goalsView;
        }

        protected override GameObject GetPieceInstance()
        {
            return _boardView.GetPieceInstance(_piece);
        }

        protected override void DestroyPiece()
        {
            _boardView.DestroyPiece(_piece);
            _goalsView.TryIncreaseCurrentAmount(_piece.Type);
        }
    }
}