using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Bag.Parsing
{
    public class BagPieceEntrySerializedDataConverter : IBagPieceEntrySerializedDataConverter
    {
        public BagPieceEntry To([NotNull] BagPieceEntrySerializedData bagPieceEntrySerializedData)
        {
            ArgumentNullException.ThrowIfNull(bagPieceEntrySerializedData);

            return new BagPieceEntry(bagPieceEntrySerializedData.PieceType, bagPieceEntrySerializedData.Amount);
        }

        public BagPieceEntrySerializedData From([NotNull] BagPieceEntry bagPieceEntry)
        {
            ArgumentNullException.ThrowIfNull(bagPieceEntry);

            return new BagPieceEntrySerializedData { PieceType = bagPieceEntry.PieceType, Amount = bagPieceEntry.Amount };
        }
    }
}