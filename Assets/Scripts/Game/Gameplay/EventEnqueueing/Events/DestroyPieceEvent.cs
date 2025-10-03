using Game.Gameplay.EventEnqueueing.Events.Reasons;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class DestroyPieceEvent : IEvent
    {
        public readonly int Id;
        public readonly DestroyPieceReason DestroyPieceReason;

        public DestroyPieceEvent(int id, DestroyPieceReason destroyPieceReason)
        {
            Id = id;
            DestroyPieceReason = destroyPieceReason;
        }
    }
}