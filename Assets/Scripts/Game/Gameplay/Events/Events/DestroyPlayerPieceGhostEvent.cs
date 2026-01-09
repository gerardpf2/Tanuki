using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.Events.Events
{
    public class DestroyPlayerPieceGhostEvent : IEvent
    {
        public readonly DestroyPieceReason DestroyPieceReason;

        public DestroyPlayerPieceGhostEvent(DestroyPieceReason destroyPieceReason)
        {
            DestroyPieceReason = destroyPieceReason;
        }
    }
}