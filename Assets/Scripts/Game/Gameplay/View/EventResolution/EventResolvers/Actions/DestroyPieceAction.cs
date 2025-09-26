using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Game.Gameplay.View.Board;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public class DestroyPieceAction : BaseDestroyPieceAction
    {
        [NotNull] private readonly IPiece _piece;
        [NotNull] private readonly IBoardView _boardView;

        public DestroyPieceAction(
            DestroyPieceReason destroyPieceReason,
            [NotNull] IPiece piece,
            [NotNull] IBoardView boardView) : base(destroyPieceReason)
        {
            ArgumentNullException.ThrowIfNull(piece);
            ArgumentNullException.ThrowIfNull(boardView);

            _piece = piece;
            _boardView = boardView;
        }

        protected override GameObject GetPieceInstance()
        {
            return _boardView.GetPieceInstance(_piece);
        }

        protected override void DestroyPiece()
        {
            _boardView.DestroyPiece(_piece);
            // TODO: GoalsView
        }
    }
}