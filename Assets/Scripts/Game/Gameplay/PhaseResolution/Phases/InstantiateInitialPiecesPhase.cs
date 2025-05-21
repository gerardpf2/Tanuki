using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class InstantiateInitialPiecesPhase : Phase, IInstantiateInitialPiecesPhase
    {
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;

        private IBoard _board;
        private IEnumerable<IPiecePlacement> _piecePlacements;

        public InstantiateInitialPiecesPhase(
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory) : base(1, -1)
        {
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);

            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
        }

        public void Initialize(
            [NotNull] IBoard board,
            [NotNull, ItemNotNull] IEnumerable<IPiecePlacement> piecePlacements)
        {
            // TODO: Check allow multiple Initialize. Add Clear ¿?

            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(piecePlacements);

            ICollection<IPiecePlacement> piecePlacementsCopy = new List<IPiecePlacement>();

            foreach (IPiecePlacement piecePlacement in piecePlacements)
            {
                ArgumentNullException.ThrowIfNull(piecePlacement);

                piecePlacementsCopy.Add(piecePlacement);
            }

            _board = board;
            _piecePlacements = piecePlacementsCopy;
        }

        protected override bool ResolveImpl(ResolveContext _)
        {
            InvalidOperationException.ThrowIfNull(_board);
            InvalidOperationException.ThrowIfNull(_piecePlacements);

            foreach (IPiecePlacement piecePlacement in _piecePlacements)
            {
                InvalidOperationException.ThrowIfNull(piecePlacement);

                IPiece piece = piecePlacement.Piece;
                Coordinate sourceCoordinate = new(piecePlacement.Row, piecePlacement.Column);

                _board.Add(piece, sourceCoordinate);

                _eventEnqueuer.Enqueue(
                    _eventFactory.GetInstantiatePieceEvent(
                        piece,
                        sourceCoordinate,
                        InstantiatePieceReason.Initial
                    )
                );
            }

            return true;
        }
    }
}