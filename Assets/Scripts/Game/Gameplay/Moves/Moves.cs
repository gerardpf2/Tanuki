namespace Game.Gameplay.Moves
{
    public class Moves : IMoves
    {
        public int Amount { get; set; }

        public void Reset()
        {
            Amount = 0;
        }
    }
}