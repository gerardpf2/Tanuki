namespace Game.Gameplay.Events.Events
{
    public class SetMovesAmountEvent : IEvent
    {
        public readonly int MovesAmount;

        public SetMovesAmountEvent(int movesAmount)
        {
            MovesAmount = movesAmount;
        }
    }
}