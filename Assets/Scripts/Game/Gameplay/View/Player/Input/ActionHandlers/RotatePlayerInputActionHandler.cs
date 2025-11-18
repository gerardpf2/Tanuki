using Game.Gameplay.Phases;
using Game.Gameplay.View.EventResolvers;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Player.Input.ActionHandlers
{
    public class RotatePlayerInputActionHandler : BasePlayerInputActionHandler
    {
        [NotNull] private readonly IPlayerPieceView _playerPieceView;

        public RotatePlayerInputActionHandler(
            [NotNull] IPhaseContainer phaseContainer,
            [NotNull] IEventsResolver eventsResolver,
            [NotNull] IPlayerPieceGhostView playerPieceGhostView,
            [NotNull] IPlayerPieceView playerPieceView) : base(phaseContainer, eventsResolver, playerPieceGhostView, playerPieceView)
        {
            ArgumentNullException.ThrowIfNull(playerPieceView);

            _playerPieceView = playerPieceView;
        }

        protected override bool GetAvailable()
        {
            return base.GetAvailable() && _playerPieceView.CanRotate();
        }

        protected override void ResolveImpl()
        {
            base.ResolveImpl();

            _playerPieceView.Rotate();
        }
    }
}