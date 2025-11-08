using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Camera;
using Game.Gameplay.Events;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Phases.Phases
{
    public class InstantiatePlayerPiecePhase : Phase
    {
        [NotNull] private readonly IBagContainer _bagContainer;
        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly ICamera _camera;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;

        protected override int? MaxResolveTimesPerIteration => 1;

        public InstantiatePlayerPiecePhase(
            [NotNull] IBagContainer bagContainer,
            [NotNull] IBoardContainer boardContainer,
            [NotNull] ICamera camera,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory)
        {
            ArgumentNullException.ThrowIfNull(bagContainer);
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(camera);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);

            _bagContainer = bagContainer;
            _boardContainer = boardContainer;
            _camera = camera;
            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
        }

        protected override ResolveResult ResolveImpl(ResolveContext _)
        {
            IPiece piece = GetPiece();
            Coordinate sourceCoordinate = GetSourceCoordinate(piece);

            _eventEnqueuer.Enqueue(_eventFactory.GetInstantiatePlayerPieceEvent(piece, sourceCoordinate));

            return ResolveResult.Updated;
        }

        [NotNull]
        private IPiece GetPiece()
        {
            IBag bag = _bagContainer.Bag;

            InvalidOperationException.ThrowIfNull(bag);

            return bag.Current;
        }

        private Coordinate GetSourceCoordinate([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            int row = _camera.TopRow - piece.Height + 1;
            int column = (board.Columns - piece.Width + 1) / 2;

            return new Coordinate(row, column);
        }
    }
}