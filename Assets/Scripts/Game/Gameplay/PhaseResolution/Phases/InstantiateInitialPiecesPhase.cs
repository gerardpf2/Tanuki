using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Events;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class InstantiateInitialPiecesPhase : Phase
    {
        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;

        protected override int? MaxResolveTimes => 1;

        public InstantiateInitialPiecesPhase(
            [NotNull] IBoardContainer boardContainer,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory)
        {
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);

            _boardContainer = boardContainer;
            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
        }

        protected override ResolveResult ResolveImpl(ResolveContext _)
        {
            IBoard board = _boardContainer.Board;
            IEnumerable<PiecePlacement> piecePlacements = _boardContainer.PiecePlacements;

            InvalidOperationException.ThrowIfNull(board);
            InvalidOperationException.ThrowIfNull(piecePlacements);

            foreach (PiecePlacement piecePlacement in piecePlacements)
            {
                InvalidOperationException.ThrowIfNull(piecePlacement);

                IPiece piece = piecePlacement.Piece;
                Coordinate sourceCoordinate = piecePlacement.Coordinate;

                board.AddPiece(piece, sourceCoordinate);

                _eventEnqueuer.Enqueue(
                    _eventFactory.GetInstantiatePieceEvent(
                        piece,
                        sourceCoordinate,
                        InstantiatePieceReason.Initial
                    )
                );
            }

            return ResolveResult.Updated;
        }
    }
}