using Game.Gameplay.Board;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.Events;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Phases.Phases
{
    public class InstantiateInitialPiecesPhase : Phase
    {
        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;

        protected override int? MaxResolveTimesPerIteration => 1;

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

            InvalidOperationException.ThrowIfNull(board);

            foreach (int pieceId in board.GetDistinctPieceIdsSortedByRowThenByColumn())
            {
                IPiece piece = board.GetPiece(pieceId);
                Coordinate sourceCoordinate = board.GetSourceCoordinate(pieceId);

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