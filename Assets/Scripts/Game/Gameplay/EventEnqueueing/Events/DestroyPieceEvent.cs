using Game.Gameplay.EventEnqueueing.Events.Reasons;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class DestroyPieceEvent : IEvent
    {
        public readonly uint Id;
        public readonly DestroyPieceReason DestroyPieceReason;

        public DestroyPieceEvent(uint id, DestroyPieceReason destroyPieceReason)
        {
            Id = id;
            DestroyPieceReason = destroyPieceReason;
        }
    }
}