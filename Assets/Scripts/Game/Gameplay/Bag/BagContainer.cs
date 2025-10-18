using Game.Common;

namespace Game.Gameplay.Bag
{
    public class BagContainer : IBagContainer
    {
        private InitializedLabel _initializedLabel;

        public IBag Bag { get; private set; }

        public void Initialize(IBag bag)
        {
            _initializedLabel.SetInitialized();

            Bag = bag;
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            Bag = null;
        }
    }
}