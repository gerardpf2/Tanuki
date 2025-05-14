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
        [NotNull] private readonly IPieceGetter _pieceGetter;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;

        private IBoard _board;
        private IEnumerable<IPiecePlacement> _piecePlacements;
        private bool _resolved;

        public InstantiateInitialPiecesPhase(
            [NotNull] IPieceGetter pieceGetter,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory)
        {
            ArgumentNullException.ThrowIfNull(pieceGetter);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);

            _pieceGetter = pieceGetter;
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

        public override bool Resolve()
        {
            if (_resolved)
            {
                return false;
            }

            ResolveImpl();

            _resolved = true;

            return true;
        }

        private void ResolveImpl()
        {
            InvalidOperationException.ThrowIfNull(_board);
            InvalidOperationException.ThrowIfNull(_piecePlacements);

            foreach (IPiecePlacement piecePlacement in _piecePlacements)
            {
                InvalidOperationException.ThrowIfNull(piecePlacement);

                IPiece piece = _pieceGetter.Get(piecePlacement.PieceType);
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
        }
    }
}