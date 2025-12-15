using JetBrains.Annotations;

namespace Game.Gameplay.Bag.Parsing
{
    public interface IBagPieceEntrySerializedDataConverter
    {
        [NotNull]
        BagPieceEntry To(BagPieceEntrySerializedData bagPieceEntrySerializedData);

        [NotNull]
        BagPieceEntrySerializedData From(BagPieceEntry bagPieceEntry);
    }
}