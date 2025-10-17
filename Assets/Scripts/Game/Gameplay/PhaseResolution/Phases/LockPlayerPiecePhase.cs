using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.EventEnqueueing;
using Game.Gameplay.Moves;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class LockPlayerPiecePhase : Phase
    {
        [NotNull] private readonly IBagContainer _bagContainer;
        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;
        [NotNull] private readonly IMovesContainer _movesContainer;

        protected override int? MaxResolveTimesPerIteration => 1;

        public LockPlayerPiecePhase(
            [NotNull] IBagContainer bagContainer,
            [NotNull] IBoardContainer boardContainer,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory,
            [NotNull] IMovesContainer movesContainer)
        {
            ArgumentNullException.ThrowIfNull(bagContainer);
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);
            ArgumentNullException.ThrowIfNull(movesContainer);

            _bagContainer = bagContainer;
            _boardContainer = boardContainer;
            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
            _movesContainer = movesContainer;
        }

        protected override ResolveResult ResolveImpl([NotNull] ResolveContext resolveContext)
        {
            ArgumentNullException.ThrowIfNull(resolveContext);

            IBag bag = _bagContainer.Bag;
            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(bag);
            InvalidOperationException.ThrowIfNull(board);

            if (!resolveContext.PieceSourceCoordinate.HasValue)
            {
                return ResolveResult.NotUpdated;
            }

            IPiece piece = bag.Current;

            bag.ConsumeCurrent();

            Coordinate lockSourceCoordinate =
                GetLockSourceCoordinate(
                    piece,
                    board,
                    resolveContext.PieceSourceCoordinate.Value
                );

            board.AddPiece(piece, lockSourceCoordinate);

            int movesAmount = DecreaseMovesAmount();

            _eventEnqueuer.Enqueue(
                _eventFactory.GetLockPlayerPieceEvent(
                    piece,
                    lockSourceCoordinate,
                    movesAmount
                )
            );

            return ResolveResult.Updated;
        }

        private static Coordinate GetLockSourceCoordinate(
            [NotNull] IPiece piece,
            [NotNull] IBoard board,
            Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(piece);
            ArgumentNullException.ThrowIfNull(board);

            int fall = board.ComputePieceFall(piece, sourceCoordinate);
            Coordinate lockSourceCoordinate = sourceCoordinate.Down(fall);

            return lockSourceCoordinate;
        }

        private int DecreaseMovesAmount()
        {
            IMoves moves = _movesContainer.Moves;

            InvalidOperationException.ThrowIfNull(moves);

            return --moves.Amount;
        }
    }
}