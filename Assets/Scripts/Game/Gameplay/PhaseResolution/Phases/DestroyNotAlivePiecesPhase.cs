using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.EventEnqueueing;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Game.Gameplay.Goals;
using Game.Gameplay.Goals.Utils;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class DestroyNotAlivePiecesPhase : Phase
    {
        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;
        [NotNull] private readonly IGoalsContainer _goalsContainer;

        public DestroyNotAlivePiecesPhase(
            [NotNull] IBoardContainer boardContainer,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory,
            [NotNull] IGoalsContainer goalsContainer)
        {
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);
            ArgumentNullException.ThrowIfNull(goalsContainer);

            _boardContainer = boardContainer;
            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
            _goalsContainer = goalsContainer;
        }

        protected override ResolveResult ResolveImpl(ResolveContext _)
        {
            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            bool resolved = false;

            foreach (int pieceId in board.GetPieceIdsSortedByRowThenByColumn())
            {
                resolved = TryDestroyPiece(pieceId) || resolved;
            }

            return resolved ? ResolveResult.Updated : ResolveResult.NotUpdated;
        }

        private bool TryDestroyPiece(int pieceId)
        {
            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            IPiece piece = board.GetPiece(pieceId);

            if (piece.Alive)
            {
                return false;
            }

            Coordinate sourceCoordinate = board.GetSourceCoordinate(pieceId);

            board.RemovePiece(pieceId);

            _eventEnqueuer.Enqueue(_eventFactory.GetDestroyPieceEvent(pieceId, DestroyPieceReason.NotAlive));

            TryIncreaseGoalCurrentAmount(piece.Type, sourceCoordinate); // TODO: Use center coordinate instead ¿?

            return true;
        }

        private void TryIncreaseGoalCurrentAmount(PieceType pieceType, Coordinate coordinate)
        {
            IGoals goals = _goalsContainer.Goals;

            InvalidOperationException.ThrowIfNull(goals);

            if (!goals.TryIncreaseCurrentAmount(pieceType, out int currentAmount))
            {
                return;
            }

            _eventEnqueuer.Enqueue(_eventFactory.GetSetGoalCurrentAmountEvent(pieceType, currentAmount, coordinate));
        }
    }
}