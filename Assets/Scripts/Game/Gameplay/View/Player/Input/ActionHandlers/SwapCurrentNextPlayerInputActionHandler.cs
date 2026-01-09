using Game.Gameplay.Phases;
using Game.Gameplay.View.EventResolvers;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Player.Input.ActionHandlers
{
    public class SwapCurrentNextPlayerInputActionHandler : BasePlayerInputActionHandler
    {
        public SwapCurrentNextPlayerInputActionHandler(
            [NotNull] IPhaseContainer phaseContainer,
            [NotNull] IEventsResolver eventsResolver,
            [NotNull] IPlayerPieceGhostView playerPieceGhostView,
            [NotNull] IPlayerPieceView playerPieceView) : base(phaseContainer, eventsResolver, playerPieceGhostView, playerPieceView, ResolveReason.SwapCurrentNext) { }
    }
}