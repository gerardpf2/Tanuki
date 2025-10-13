using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.EventEnqueueing;
using Game.Gameplay.Moves;
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
        [NotNull] private readonly IMovesContainer _movesContainer;
        [NotNull] private readonly IPlayerPiecesBag _playerPiecesBag;

        private IPiece _targetPiece;

        protected override int? MaxResolveTimesPerIteration => 1;

        public LockPlayerPiecePhase(
            [NotNull] IBoardContainer boardContainer,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory,
            [NotNull] IMovesContainer movesContainer,
            [NotNull] IPlayerPiecesBag playerPiecesBag)
        {
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);
            ArgumentNullException.ThrowIfNull(movesContainer);
            ArgumentNullException.ThrowIfNull(playerPiecesBag);

            _boardContainer = boardContainer;
            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
            _movesContainer = movesContainer;
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

            board.AddPiece(_targetPiece, lockSourceCoordinate);

            int movesAmount = DecreaseMovesAmount();

            _eventEnqueuer.Enqueue(
                _eventFactory.GetLockPlayerPieceEvent(
                    _targetPiece,
                    lockSourceCoordinate,
                    movesAmount
                )
            );

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

        private int DecreaseMovesAmount()
        {
            IMoves moves = _movesContainer.Moves;

            InvalidOperationException.ThrowIfNull(moves);

            return --moves.Amount;
        }
    }
}