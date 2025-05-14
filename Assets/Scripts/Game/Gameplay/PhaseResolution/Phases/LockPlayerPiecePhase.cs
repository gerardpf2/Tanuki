using Game.Gameplay.Player;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class LockPlayerPiecePhase : Phase, ILockPlayerPiecePhase
    {
        [NotNull] private readonly IPlayerPiecesBag _playerPiecesBag;

        public LockPlayerPiecePhase([NotNull] IPlayerPiecesBag playerPiecesBag)
        {
            ArgumentNullException.ThrowIfNull(playerPiecesBag);

            _playerPiecesBag = playerPiecesBag;
        }

        public void Initialize()
        {
            // TODO: Check allow multiple Initialize. Add Clear ¿?
        }

        public override bool Resolve()
        {
            if (_playerPiecesBag.Current is null)
            {
                return false;
            }

            _playerPiecesBag.ConsumeCurrent();

            return true;
        }
    }
}