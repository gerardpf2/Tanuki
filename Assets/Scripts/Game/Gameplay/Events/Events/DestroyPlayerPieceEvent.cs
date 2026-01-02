using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.Events.Events
{
    public class DestroyPlayerPieceEvent : IEvent
    {
        public readonly DestroyPieceReason DestroyPieceReason;

        public DestroyPlayerPieceEvent(DestroyPieceReason destroyPieceReason)
        {
            DestroyPieceReason = destroyPieceReason;
        }
    }
}