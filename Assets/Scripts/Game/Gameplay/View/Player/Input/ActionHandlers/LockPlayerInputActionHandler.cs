using Game.Gameplay.Phases;
using Game.Gameplay.View.EventResolvers;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Player.Input.ActionHandlers
{
    public class LockPlayerInputActionHandler : BasePlayerInputActionHandler
    {
        public LockPlayerInputActionHandler(
            [NotNull] IPhaseContainer phaseContainerLock,
            [NotNull] IEventsResolver eventsResolver,
            [NotNull] IPlayerPieceGhostView playerPieceGhostView,
            [NotNull] IPlayerPieceView playerPieceView) : base(phaseContainerLock, eventsResolver, playerPieceGhostView, playerPieceView) { }
    }
}