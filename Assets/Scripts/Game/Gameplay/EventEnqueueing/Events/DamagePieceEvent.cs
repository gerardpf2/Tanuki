namespace Game.Gameplay.EventEnqueueing.Events
{
    public class DamagePieceEvent : IEvent
    {
        public readonly uint Id;

        public DamagePieceEvent(uint id)
        {
            Id = id;
        }
    }
}