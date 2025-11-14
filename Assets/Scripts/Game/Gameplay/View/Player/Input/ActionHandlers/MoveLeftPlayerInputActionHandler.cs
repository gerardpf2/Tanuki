using Game.Gameplay.Phases;
using Game.Gameplay.View.EventResolvers;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Player.Input.ActionHandlers
{
    public class MoveLeftPlayerInputActionHandler : MovePlayerInputActionHandler
    {
        public MoveLeftPlayerInputActionHandler(
            [NotNull] IPhaseContainer phaseContainerMove,
            [NotNull] IEventsResolver eventsResolver,
            [NotNull] IPlayerPieceGhostView playerPieceGhostView,
            [NotNull] IPlayerPieceView playerPieceView) : base(phaseContainerMove, eventsResolver, playerPieceGhostView, playerPieceView, -1) { }
    }
}