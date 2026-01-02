using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.Events.Events
{
    public class DestroyPlayerPieceEvent : IEvent
    {
        public readonly DestroyPlayerPieceGhostEvent DestroyPlayerPieceGhostEvent;
        public readonly DestroyPieceReason DestroyPieceReason;

        public DestroyPlayerPieceEvent(
            DestroyPlayerPieceGhostEvent destroyPlayerPieceGhostEvent,
            DestroyPieceReason destroyPieceReason)
        {
            DestroyPlayerPieceGhostEvent = destroyPlayerPieceGhostEvent;
            DestroyPieceReason = destroyPieceReason;
        }
    }
}