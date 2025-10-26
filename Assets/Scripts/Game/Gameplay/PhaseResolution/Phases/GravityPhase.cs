using Game.Gameplay.Board;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.EventEnqueueing;
using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.Pieces.Pieces;
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
            bool resolved = false;

            MovePiecesByGravityEvent movePiecesByGravityEvent = _eventFactory.GetMovePiecesByGravityEvent();

            while (TryMovePieces(movePiecesByGravityEvent))
            {
                resolved = true;
            }

            if (!resolved)
            {
                return ResolveResult.NotUpdated;
            }

            _eventEnqueuer.Enqueue(movePiecesByGravityEvent);

            return ResolveResult.Updated;
        }

        private bool TryMovePieces([NotNull] MovePiecesByGravityEvent movePiecesByGravityEvent)
        {
            ArgumentNullException.ThrowIfNull(movePiecesByGravityEvent);

            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            bool resolved = false;

            foreach (int pieceId in board.GetDistinctPieceIdsSortedByRowThenByColumn())
            {
                if (TryMovePiece(movePiecesByGravityEvent, pieceId))
                {
                    resolved = true;
                }
            }

            return resolved;
        }

        private bool TryMovePiece([NotNull] MovePiecesByGravityEvent movePiecesByGravityEvent, int pieceId)
        {
            ArgumentNullException.ThrowIfNull(movePiecesByGravityEvent);

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

            MovePiecesByGravityEvent.PieceMovementData pieceMovementData = new(pieceId, rowOffset, columnOffset);

            movePiecesByGravityEvent.Add(pieceMovementData);

            return true;
        }
    }
}