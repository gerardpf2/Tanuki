namespace Game.Gameplay.Moves
{
    public interface IMoves
    {
        int Amount { get; set; }

        void Reset();
    }
}