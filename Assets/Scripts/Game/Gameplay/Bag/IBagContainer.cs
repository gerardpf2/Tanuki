namespace Game.Gameplay.Bag
{
    public interface IBagContainer
    {
        IBag Bag { get; }

        void Initialize(IBag bag);

        void Uninitialize();
    }
}