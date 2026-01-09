using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Events;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Moves;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Phases.Phases
{
    public class LockPlayerPiecePhase : Phase
    {
        [NotNull] private readonly IBag _bag;
        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IMoves _moves;

        protected override int? MaxResolveTimesPerIteration => 1;

        public LockPlayerPiecePhase(
            [NotNull] IBag bag,
            [NotNull] IBoard board,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IMoves moves)
        {
            ArgumentNullException.ThrowIfNull(bag);
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(moves);

            _bag = bag;
            _board = board;
            _eventEnqueuer = eventEnqueuer;
            _moves = moves;
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

            LockPlayerPieceEvent lockPlayerPieceEvent = new(piece, sourceCoordinate, lockSourceCoordinate, movesAmount);

            _eventEnqueuer.Enqueue(lockPlayerPieceEvent);

            return ResolveResult.Updated;
        }

        [NotNull]
        private IPiece ConsumeCurrentBagPiece()
        {
            IPiece piece = _bag.Current;

            _bag.ConsumeCurrent();

            return piece;
        }

        private void AddPieceToBoard(IPiece piece, Coordinate sourceCoordinate)
        {
            _board.AddPiece(piece, sourceCoordinate);
        }

        private int DecreaseMovesAmount()
        {
            return --_moves.Amount;
        }
    }
}