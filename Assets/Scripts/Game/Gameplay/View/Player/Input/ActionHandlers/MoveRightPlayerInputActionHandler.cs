using Game.Gameplay.Phases;
using Game.Gameplay.View.EventResolvers;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Player.Input.ActionHandlers
{
    public class MoveRightPlayerInputActionHandler : MovePlayerInputActionHandler
    {
        public MoveRightPlayerInputActionHandler(
            [NotNull] IPhaseContainer phaseContainer,
            [NotNull] IEventsResolver eventsResolver,
            [NotNull] IPlayerPieceGhostView playerPieceGhostView,
            [NotNull] IPlayerPieceView playerPieceView) : base(phaseContainer, eventsResolver, playerPieceGhostView, playerPieceView, 1) { }
    }
}