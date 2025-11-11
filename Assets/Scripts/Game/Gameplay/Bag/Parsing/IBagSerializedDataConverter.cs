using JetBrains.Annotations;

namespace Game.Gameplay.Bag.Parsing
{
    public interface IBagSerializedDataConverter
    {
        void To(BagSerializedData bagSerializedData, IBag bag);

        [NotNull]
        BagSerializedData From(IBag bag);
    }
}