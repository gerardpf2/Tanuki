using Game.Gameplay.Events.Reasons;
using Game.Gameplay.View.Board;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public class DestroyPieceAction : BaseDestroyPieceAction
    {
        private readonly int _pieceId;
        [NotNull] private readonly IBoardView _boardView;

        public DestroyPieceAction(DestroyPieceReason destroyPieceReason, int pieceId, [NotNull] IBoardView boardView) : base(destroyPieceReason)
        {
            ArgumentNullException.ThrowIfNull(boardView);

            _pieceId = pieceId;
            _boardView = boardView;
        }

        protected override GameObject GetPieceInstance()
        {
            return _boardView.GetPieceInstance(_pieceId);
        }

        protected override void DestroyPiece()
        {
            _boardView.DestroyPiece(_pieceId);
        }
    }
}