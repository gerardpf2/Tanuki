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
        private readonly int _id;
        [NotNull] private readonly IBoardView _boardView;
        [NotNull] private readonly IGoalsView _goalsView;

        public DestroyPieceAction(
            DestroyPieceReason destroyPieceReason,
            int id,
            [NotNull] IBoardView boardView,
            [NotNull] IGoalsView goalsView) : base(destroyPieceReason)
        {
            ArgumentNullException.ThrowIfNull(boardView);
            ArgumentNullException.ThrowIfNull(goalsView);

            _id = id;
            _boardView = boardView;
            _goalsView = goalsView;
        }

        protected override GameObject GetPieceInstance()
        {
            return _boardView.GetPieceInstance(_id);
        }

        protected override void DestroyPiece()
        {
            IPiece piece = _boardView.GetPiece(_id);

            _boardView.DestroyPiece(_id);
            _goalsView.TryIncreaseCurrentAmount(piece.Type);
        }
    }
}