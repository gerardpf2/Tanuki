namespace Game.Gameplay.Moves
{
    public class Moves : IMoves
    {
        public int Amount { get; set; }

        public Moves(int amount)
        {
            Amount = amount;
        }
    }
}