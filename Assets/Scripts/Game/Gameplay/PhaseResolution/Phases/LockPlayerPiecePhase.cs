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
            // TODO: Check allow multiple Initialize. Add Clear ¿?

            ArgumentNullException.ThrowIfNull(board);

            _board = board;
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

            IPiece piece = _playerPiecesBag.Current;

            _playerPiecesBag.ConsumeCurrent();

            Coordinate lockSourceCoordinate = GetLockSourceCoordinate(piece, resolveContext.Column.Value);

            _board.Add(piece, lockSourceCoordinate); // TODO: Resize if needed

            _eventEnqueuer.Enqueue(_eventFactory.GetLockPlayerPieceEvent(piece, lockSourceCoordinate));

            return true;
        }

        public override void OnEndIteration()
        {
            base.OnEndIteration();

            _targetPiece = null;
        }

        private Coordinate GetLockSourceCoordinate([NotNull] IPiece piece, int column)
        {
            ArgumentNullException.ThrowIfNull(piece);
            InvalidOperationException.ThrowIfNull(_board);

            Coordinate sourceCoordinate = new(_board.Rows, column);
            int fall = _board.ComputeFall(piece, sourceCoordinate);
            Coordinate lockSourceCoordinate = new(sourceCoordinate.Row - fall, sourceCoordinate.Column);

            return lockSourceCoordinate;
        }
    }
}