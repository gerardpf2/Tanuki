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
        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;

        protected override int? MaxResolveTimesPerIteration => 1;

        public InstantiateInitialPiecesPhase(
            [NotNull] IBoard board,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);

            _board = board;
            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
        }

        protected override ResolveResult ResolveImpl(ResolveContext _)
        {
            foreach (int pieceId in _board.GetDistinctPieceIdsSortedByRowThenByColumn())
            {
                IPiece piece = _board.GetPiece(pieceId);
                Coordinate sourceCoordinate = _board.GetSourceCoordinate(pieceId);

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