using System.Linq;
using Game.Gameplay.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Bag.Parsing
{
    public class BagSerializedDataConverter : IBagSerializedDataConverter
    {
        [NotNull] private readonly IBagPieceEntrySerializedDataConverter _bagPieceEntrySerializedDataConverter;
        [NotNull] private readonly IPieceGetter _pieceGetter;

        public BagSerializedDataConverter(
            [NotNull] IBagPieceEntrySerializedDataConverter bagPieceEntrySerializedDataConverter,
            [NotNull] IPieceGetter pieceGetter)
        {
            ArgumentNullException.ThrowIfNull(bagPieceEntrySerializedDataConverter);
            ArgumentNullException.ThrowIfNull(pieceGetter);

            _bagPieceEntrySerializedDataConverter = bagPieceEntrySerializedDataConverter;
            _pieceGetter = pieceGetter;
        }

        public IBag To([NotNull] BagSerializedData bagSerializedData)
        {
            ArgumentNullException.ThrowIfNull(bagSerializedData);

            IBag bag = new Bag(_pieceGetter);

            bag.Build(
                bagSerializedData.BagPieceEntries.Select(_bagPieceEntrySerializedDataConverter.To),
                bagSerializedData.InitialPieceTypes
            );

            return bag;
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