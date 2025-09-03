using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.EventEnqueueing;
using Game.Gameplay.Player;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class LockPlayerPiecePhase : Phase, ILockPlayerPiecePhase
    {
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;
        [NotNull] private readonly IPlayerPiecesBag _playerPiecesBag;

        private IBoard _board;
        private IPiece _targetPiece;

        public LockPlayerPiecePhase(
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory,
            [NotNull] IPlayerPiecesBag playerPiecesBag) : base(-1, 1)
        {
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);
            ArgumentNullException.ThrowIfNull(playerPiecesBag);

            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
            _playerPiecesBag = playerPiecesBag;
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

        public override void OnBeginIteration()
        {
            base.OnBeginIteration();

            _targetPiece = _playerPiecesBag.Current;
        }

        protected override bool ResolveImpl([NotNull] ResolveContext resolveContext)
        {
            ArgumentNullException.ThrowIfNull(resolveContext);
            InvalidOperationException.ThrowIfNull(_board);

            if (_targetPiece is null || _playerPiecesBag.Current != _targetPiece || !resolveContext.Column.HasValue)
            {
                return false;
            }

            _playerPiecesBag.ConsumeCurrent();

            Coordinate lockSourceCoordinate = GetLockSourceCoordinate(resolveContext.Column.Value);

            _board.Add(_targetPiece, lockSourceCoordinate);

            _eventEnqueuer.Enqueue(_eventFactory.GetLockPlayerPieceEvent(_targetPiece, lockSourceCoordinate));

            return true;
        }

        public override void OnEndIteration()
        {
            base.OnEndIteration();

            _targetPiece = null;
        }

        private Coordinate GetLockSourceCoordinate(int column)
        {
            InvalidOperationException.ThrowIfNull(_board);
            InvalidOperationException.ThrowIfNull(_targetPiece);

            Coordinate sourceCoordinate = new(_board.Rows, column);
            int fall = _board.ComputePieceFall(_targetPiece, sourceCoordinate);
            Coordinate lockSourceCoordinate = new(sourceCoordinate.Row - fall, sourceCoordinate.Column);

            return lockSourceCoordinate;
        }
    }
}