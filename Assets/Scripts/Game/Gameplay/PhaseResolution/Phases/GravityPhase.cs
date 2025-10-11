using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.EventEnqueueing;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class GravityPhase : Phase
    {
        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;

        public GravityPhase(
            [NotNull] IBoardContainer boardContainer,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory)
        {
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);

            _boardContainer = boardContainer;
            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
        }

        protected override ResolveResult ResolveImpl(ResolveContext _)
        {
            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            bool resolved = false;

            foreach (int pieceId in board.GetPieceIdsSortedByRowThenByColumn())
            {
                resolved = TryMovePiece(pieceId) || resolved;
            }

            return resolved ? ResolveResult.Updated : ResolveResult.NotUpdated;
        }

        private bool TryMovePiece(int pieceId)
        {
            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            IPiece piece = board.GetPiece(pieceId);
            Coordinate sourceCoordinate = board.GetSourceCoordinate(pieceId);

            int fall = board.ComputePieceFall(piece, sourceCoordinate);

            if (fall <= 0)
            {
                return false;
            }

            int rowOffset = -fall;
            const int columnOffset = 0;

            board.MovePiece(pieceId, rowOffset, columnOffset);

            _eventEnqueuer.Enqueue(
                _eventFactory.GetMovePieceEvent(
                    pieceId,
                    rowOffset,
                    columnOffset,
                    MovePieceReason.Gravity
                )
            );

            return true;
        }
    }
}