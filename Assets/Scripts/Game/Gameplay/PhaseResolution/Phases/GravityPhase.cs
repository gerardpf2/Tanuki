using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.EventEnqueueing;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class GravityPhase : Phase, IGravityPhase
    {
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;

        private IBoard _board;

        public GravityPhase([NotNull] IEventEnqueuer eventEnqueuer, [NotNull] IEventFactory eventFactory) : base(-1, -1)
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
                IPiece piece = piecePlacement.Piece;

                InvalidOperationException.ThrowIfNull(piece);

                resolved = TryMovePiece(piece) || resolved;
            }

            return resolved;
        }

        private bool TryMovePiece([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            Coordinate sourceCoordinate = _board.GetPieceSourceCoordinate(piece);
            int fall = _board.ComputePieceFall(piece, sourceCoordinate);

            if (fall == 0)
            {
                return false;
            }

            _board.Move(piece, -fall, 0);

            // TODO: EventEnqueuer

            return true;
        }
    }
}