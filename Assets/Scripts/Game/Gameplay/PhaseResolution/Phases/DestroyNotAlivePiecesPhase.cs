using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.EventEnqueueing;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Game.Gameplay.Goals;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class DestroyNotAlivePiecesPhase : Phase, IDestroyNotAlivePiecesPhase
    {
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;
        [NotNull] private readonly IGoalsStateContainer _goalsStateContainer;

        private IBoard _board;

        public DestroyNotAlivePiecesPhase(
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory,
            [NotNull] IGoalsStateContainer goalsStateContainer) : base(-1, -1)
        {
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);
            ArgumentNullException.ThrowIfNull(goalsStateContainer);

            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
            _goalsStateContainer = goalsStateContainer;
        }

        public void Initialize([NotNull] IBoard board)
        {
            ArgumentNullException.ThrowIfNull(board);

            Uninitialize();

            _board = board;
        }

        public override void Uninitialize()
        {
            base.Uninitialize();

            _board = null;
        }

        protected override bool ResolveImpl(ResolveContext _)
        {
            InvalidOperationException.ThrowIfNull(_board);

            bool resolved = false;

            foreach (IPiece piece in _board.GetPiecesSortedByRowThenByColumn())
            {
                resolved = TryDestroyPiece(piece) || resolved;
            }

            return resolved;
        }

        private bool TryDestroyPiece([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);
            InvalidOperationException.ThrowIfNull(_board);

            if (piece.Alive)
            {
                return false;
            }

            _board.Remove(piece);

            _goalsStateContainer.TryRegisterDestroyed(piece.Type);

            _eventEnqueuer.Enqueue(_eventFactory.GetDestroyPieceEvent(piece, DestroyPieceReason.NotAlive));

            return true;
        }
    }
}