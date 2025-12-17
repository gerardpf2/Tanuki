using Game.Gameplay.Phases;
using Game.Gameplay.View.EventResolvers;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Player.Input.ActionHandlers
{
    public abstract class MovePlayerInputActionHandler : BasePlayerInputActionHandler
    {
        [NotNull] private readonly IPlayerPieceView _playerPieceView;
        private readonly int _columnOffset;

        protected MovePlayerInputActionHandler(
            [NotNull] IPhaseContainer phaseContainer,
            [NotNull] IEventsResolver eventsResolver,
            [NotNull] IPlayerPieceGhostView playerPieceGhostView,
            [NotNull] IPlayerPieceView playerPieceView,
            ResolveReason resolveReason,
            int columnOffset) : base(phaseContainer, eventsResolver, playerPieceGhostView, playerPieceView, resolveReason)
        {
            ArgumentNullException.ThrowIfNull(playerPieceView);

            _playerPieceView = playerPieceView;
            _columnOffset = columnOffset;
        }

        protected override bool GetAvailable()
        {
            return base.GetAvailable() && _playerPieceView.CanMove(_columnOffset);
        }

        protected override void ResolveImpl()
        {
            base.ResolveImpl();

            _playerPieceView.Move(_columnOffset);
        }
    }
}