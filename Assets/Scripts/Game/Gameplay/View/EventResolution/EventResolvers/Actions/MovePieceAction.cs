using System;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Game.Gameplay.View.Board;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public class MovePieceAction : IAction
    {
        [NotNull] private readonly IBoardView _boardView;
        private readonly IPiece _piece;
        private readonly int _rowOffset;
        private readonly int _columnOffset;
        private readonly MovePieceReason _movePieceReason;

        public MovePieceAction(
            [NotNull] IBoardView boardView,
            IPiece piece,
            int rowOffset,
            int columnOffset,
            MovePieceReason movePieceReason)
        {
            ArgumentNullException.ThrowIfNull(boardView);

            _boardView = boardView;
            _piece = piece;
            _rowOffset = rowOffset;
            _columnOffset = columnOffset;
            _movePieceReason = movePieceReason;
        }

        public void Resolve(Action onComplete)
        {
            // TODO

            _boardView.MovePiece(_piece, _rowOffset, _columnOffset);

            onComplete?.Invoke();
        }
    }
}