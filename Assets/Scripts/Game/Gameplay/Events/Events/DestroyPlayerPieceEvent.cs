using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.Events.Events
{
    public class DestroyPlayerPieceEvent : IEvent
    {
        public readonly DestroyPieceReason DestroyPieceReason;
        public readonly DestroyPlayerPieceGhostEvent DestroyPlayerPieceGhostEvent;

        public DestroyPlayerPieceEvent(DestroyPieceReason destroyPieceReason, bool destroyGhost = true)
        {
            DestroyPieceReason = destroyPieceReason;

            if (destroyGhost)
            {
                DestroyPlayerPieceGhostEvent = new DestroyPlayerPieceGhostEvent(destroyPieceReason);
            }
        }
    }
}