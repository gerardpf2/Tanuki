namespace Game.Gameplay.EventEnqueueing.Events
{
    public class SetMovesAmountEvent : IEvent
    {
        public readonly int Amount;

        public SetMovesAmountEvent(int amount)
        {
            Amount = amount;
        }
    }
}