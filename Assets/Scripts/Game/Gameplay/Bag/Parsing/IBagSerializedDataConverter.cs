using JetBrains.Annotations;

namespace Game.Gameplay.Bag.Parsing
{
    public interface IBagSerializedDataConverter
    {
        [NotNull]
        IBag To(BagSerializedData bagSerializedData);

        [NotNull]
        BagSerializedData From(IBag bag);
    }
}