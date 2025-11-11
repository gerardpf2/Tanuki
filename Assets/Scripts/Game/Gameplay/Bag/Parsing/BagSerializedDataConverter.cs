using System.Linq;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Bag.Parsing
{
    public class BagSerializedDataConverter : IBagSerializedDataConverter
    {
        [NotNull] private readonly IBagPieceEntrySerializedDataConverter _bagPieceEntrySerializedDataConverter;

        public BagSerializedDataConverter(
            [NotNull] IBagPieceEntrySerializedDataConverter bagPieceEntrySerializedDataConverter)
        {
            ArgumentNullException.ThrowIfNull(bagPieceEntrySerializedDataConverter);

            _bagPieceEntrySerializedDataConverter = bagPieceEntrySerializedDataConverter;
        }

        public void To([NotNull] BagSerializedData bagSerializedData, [NotNull] IBag bag)
        {
            ArgumentNullException.ThrowIfNull(bagSerializedData);
            ArgumentNullException.ThrowIfNull(bag);

            bag.Build(
                bagSerializedData.BagPieceEntries.Select(_bagPieceEntrySerializedDataConverter.To),
                bagSerializedData.InitialPieceTypes
            );
        }

        public BagSerializedData From([NotNull] IBag bag)
        {
            ArgumentNullException.ThrowIfNull(bag);

            return
                new BagSerializedData
                {
                    BagPieceEntries = bag.BagPieceEntries.Select(_bagPieceEntrySerializedDataConverter.From).ToList(),
                    InitialPieceTypes = bag.InitialPieceTypes.ToList()
                };
        }
    }
}