using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.EventEnqueueing;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class DestroyNotAlivePiecesPhase : Phase, IDestroyNotAlivePiecesPhase
    {
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;

        private IBoard _board;

        public DestroyNotAlivePiecesPhase([NotNull] IEventEnqueuer eventEnqueuer, [NotNull] IEventFactory eventFactory) : base(-1, -1)
        {
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);

            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
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

            foreach (PiecePlacement piecePlacement in _board.GetAllPieces())
            {
                InvalidOperationException.ThrowIfNull(piecePlacement.Piece);

                resolved = TryDestroyPiece(piecePlacement.Piece) || resolved;
            }

            return resolved;
        }

        private bool TryDestroyPiece([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            if (piece.Alive)
            {
                return false;
            }

            _board.Remove(piece);

            // TODO: EventEnqueuer

            return true;
        }
    }
}