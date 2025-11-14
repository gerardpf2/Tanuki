using Game.Gameplay.Phases;
using Game.Gameplay.View.EventResolvers;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Player.Input.ActionHandlers
{
    public abstract class MovePlayerInputActionHandler : BasePlayerInputActionHandler
    {
        [NotNull] private readonly IPlayerPieceView _playerPieceView;
        private readonly int _offsetX;

        protected MovePlayerInputActionHandler(
            [NotNull] IPhaseContainer phaseContainerMove,
            [NotNull] IEventsResolver eventsResolver,
            [NotNull] IPlayerPieceGhostView playerPieceGhostView,
            [NotNull] IPlayerPieceView playerPieceView,
            int offsetX) : base(phaseContainerMove, eventsResolver, playerPieceGhostView, playerPieceView)
        {
            ArgumentNullException.ThrowIfNull(playerPieceView);

            _playerPieceView = playerPieceView;
            _offsetX = offsetX;
        }

        protected override bool GetAvailable()
        {
            // TODO

            return base.GetAvailable();
        }

        protected override void ResolveImpl()
        {
            base.ResolveImpl();

            _playerPieceView.Move(_offsetX);
        }
    }
}