using Game.Gameplay.Player;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class LockPlayerPiecePhase : ILockPlayerPiecePhase
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

        public bool Resolve()
        {
            if (!_playerPiecesBag.Current.HasValue)
            {
                return false;
            }

            _playerPiecesBag.ConsumeCurrent();

            return true;
        }
    }
}