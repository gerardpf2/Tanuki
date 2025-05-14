using Game.Gameplay.Board.Pieces;
using Game.Gameplay.Player;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class LockPlayerPiecePhase : Phase, ILockPlayerPiecePhase
    {
        [NotNull] private readonly IPlayerPiecesBag _playerPiecesBag;

        private IPiece _targetPiece;

        public LockPlayerPiecePhase([NotNull] IPlayerPiecesBag playerPiecesBag) : base(-1, 1)
        {
            ArgumentNullException.ThrowIfNull(playerPiecesBag);

            _playerPiecesBag = playerPiecesBag;
        }

        public void Initialize()
        {
            // TODO: Check allow multiple Initialize. Add Clear ¿?
        }

        public override void OnBeginIteration()
        {
            base.OnBeginIteration();

            _targetPiece = _playerPiecesBag.Current;
        }

        protected override bool ResolveImpl()
        {
            if (_targetPiece is null || _playerPiecesBag.Current != _targetPiece)
            {
                return false;
            }

            _playerPiecesBag.ConsumeCurrent();

            return true;
        }

        public override void OnEndIteration()
        {
            base.OnEndIteration();

            _targetPiece = null;
        }
    }
}