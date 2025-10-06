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

            foreach (IPiece piece in board.GetPiecesSortedByRowThenByColumn())
            {
                resolved = TryDestroyPiece(piece) || resolved;
            }

            return resolved ? ResolveResult.Updated : ResolveResult.NotUpdated;
        }

        private bool TryDestroyPiece([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            IBoard board = _boardContainer.Board;
            IGoals goals = _goalsContainer.Goals;

            InvalidOperationException.ThrowIfNull(board);
            InvalidOperationException.ThrowIfNull(goals);

            if (piece.Alive)
            {
                return false;
            }

            board.Remove(piece);
            goals.TryIncreaseCurrentAmount(piece.Type);

            _eventEnqueuer.Enqueue(_eventFactory.GetDestroyPieceEvent(piece.Id, DestroyPieceReason.NotAlive));

            return true;
        }
    }
}