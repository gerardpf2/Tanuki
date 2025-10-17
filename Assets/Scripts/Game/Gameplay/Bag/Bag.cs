using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Board;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Bag
{
    public class Bag : IBag
    {
        [NotNull, ItemNotNull] private readonly IReadOnlyCollection<BagPieceEntry> _bagPieceEntries;
        [NotNull] private readonly List<PieceType> _pieceTypes = new(); // Reverse order because add / remove last should be more efficient than first

        public Bag(
            [NotNull, ItemNotNull] IEnumerable<BagPieceEntry> bagPieceEntries,
            IEnumerable<PieceType> initialPieceTypes)
        {
            ArgumentNullException.ThrowIfNull(bagPieceEntries);

            List<BagPieceEntry> bagPieceEntriesCopy = new();

            foreach (BagPieceEntry bagPieceEntry in bagPieceEntries)
            {
                ArgumentNullException.ThrowIfNull(bagPieceEntry);

                bagPieceEntriesCopy.Add(bagPieceEntry);
            }

            _bagPieceEntries = bagPieceEntriesCopy;

            Refill();

            if (initialPieceTypes is not null)
            {
                _pieceTypes.AddRange(initialPieceTypes.Reverse());
            }
        }

        public PieceType GetCurrent()
        {
            if (_pieceTypes.Count == 0)
            {
                InvalidOperationException.Throw("Cannot be empty");
            }

            return _pieceTypes[^1];
        }

        public void ConsumeCurrent()
        {
            if (_pieceTypes.Count == 0)
            {
                InvalidOperationException.Throw("Cannot be empty");
            }

            _pieceTypes.RemoveAt(_pieceTypes.Count - 1);

            if (_pieceTypes.Count == 0)
            {
                Refill();
            }
        }

        private void Refill()
        {
            foreach (BagPieceEntry bagPieceEntry in _bagPieceEntries)
            {
                for (int i = 0; i < bagPieceEntry.Amount; ++i)
                {
                    _pieceTypes.Add(bagPieceEntry.PieceType);
                }
            }

            Shuffle();
        }

        private void Shuffle()
        {
            // TODO
        }
    }
}