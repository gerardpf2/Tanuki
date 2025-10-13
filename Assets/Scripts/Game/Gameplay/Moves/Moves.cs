namespace Game.Gameplay.Moves
{
    public class Moves : IMoves
    {
        public int Amount { get; set; }

        public Moves(int amount)
        {
            Amount = amount;
        }

        public IMoves Clone()
        {
            return new Moves(Amount);
        }
    }
}