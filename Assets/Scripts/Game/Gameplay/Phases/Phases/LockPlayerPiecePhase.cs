using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Events;
using Game.Gameplay.Moves;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Phases.Phases
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

            if (!resolveContext.PieceSourceCoordinate.HasValue || !resolveContext.PieceLockSourceCoordinate.HasValue)
            {
                return ResolveResult.NotUpdated;
            }

            Coordinate sourceCoordinate = resolveContext.PieceSourceCoordinate.Value;
            Coordinate lockSourceCoordinate = resolveContext.PieceLockSourceCoordinate.Value;

            IPiece piece = ConsumeCurrentBagPiece();

            AddPieceToBoard(piece, lockSourceCoordinate);

            int movesAmount = DecreaseMovesAmount();

            _eventEnqueuer.Enqueue(
                _eventFactory.GetLockPlayerPieceEvent(
                    piece,
                    sourceCoordinate,
                    lockSourceCoordinate,
                    movesAmount
                )
            );

            return ResolveResult.Updated;
        }

        [NotNull]
        private IPiece ConsumeCurrentBagPiece()
        {
            IBag bag = _bagContainer.Bag;

            InvalidOperationException.ThrowIfNull(bag);

            IPiece piece = bag.Current;

            bag.ConsumeCurrent();

            return piece;
        }

        private void AddPieceToBoard([NotNull] IPiece piece, Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(piece);

            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            board.AddPiece(piece, sourceCoordinate);
        }

        private int DecreaseMovesAmount()
        {
            IMoves moves = _movesContainer.Moves;

            InvalidOperationException.ThrowIfNull(moves);

            return --moves.Amount;
        }
    }
}