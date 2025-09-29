using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.EventEnqueueing;
using Game.Gameplay.Player;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class LockPlayerPiecePhase : Phase
    {
        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;
        [NotNull] private readonly IPlayerPiecesBag _playerPiecesBag;

        private IPiece _targetPiece;

        public LockPlayerPiecePhase(
            [NotNull] IBoardContainer boardContainer,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory,
            [NotNull] IPlayerPiecesBag playerPiecesBag) : base(-1, 1)
        {
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);
            ArgumentNullException.ThrowIfNull(playerPiecesBag);

            _boardContainer = boardContainer;
            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
            _playerPiecesBag = playerPiecesBag;
        }

        public override void OnBeginIteration()
        {
            base.OnBeginIteration();

            _targetPiece = _playerPiecesBag.Current;
        }

        protected override ResolveResult ResolveImpl([NotNull] ResolveContext resolveContext)
        {
            ArgumentNullException.ThrowIfNull(resolveContext);

            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            if (_targetPiece is null || _playerPiecesBag.Current != _targetPiece || !resolveContext.Column.HasValue)
            {
                return ResolveResult.NotUpdated;
            }

            _playerPiecesBag.ConsumeCurrent();

            Coordinate lockSourceCoordinate = GetLockSourceCoordinate(resolveContext.Column.Value);

            board.Add(_targetPiece, lockSourceCoordinate);

            _eventEnqueuer.Enqueue(_eventFactory.GetLockPlayerPieceEvent(_targetPiece, lockSourceCoordinate));

            return ResolveResult.Updated;
        }

        public override void OnEndIteration()
        {
            base.OnEndIteration();

            _targetPiece = null;
        }

        private Coordinate GetLockSourceCoordinate(int column)
        {
            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);
            InvalidOperationException.ThrowIfNull(_targetPiece);

            Coordinate sourceCoordinate = new(board.Rows, column);
            int fall = board.ComputePieceFall(_targetPiece, sourceCoordinate);
            Coordinate lockSourceCoordinate = new(sourceCoordinate.Row - fall, sourceCoordinate.Column);

            return lockSourceCoordinate;
        }
    }
}