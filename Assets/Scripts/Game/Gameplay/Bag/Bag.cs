using System.Collections.Generic;
using Game.Gameplay.Board;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Bag
{
    public class Bag : IBag
    {
        [NotNull, ItemNotNull] private readonly IReadOnlyCollection<BagPieceEntry> _bagPieceEntries;
        [NotNull] private readonly IReadOnlyList<PieceType> _initialPieceTypes;
        [NotNull] private readonly IList<PieceType> _pieceTypes = new List<PieceType>(); // Reverse order because add / remove last should be more efficient than first

        public IEnumerable<BagPieceEntry> BagPieceEntries => _bagPieceEntries;

        public IEnumerable<PieceType> InitialPieceTypes => _initialPieceTypes;

        public PieceType Current
        {
            get
            {
                if (_pieceTypes.Count == 0)
                {
                    InvalidOperationException.Throw("Cannot be empty");
                }

                return _pieceTypes[^1];
            }
        }

        public Bag(
            [NotNull, ItemNotNull] IEnumerable<BagPieceEntry> bagPieceEntries,
            [NotNull] IEnumerable<PieceType> initialPieceTypes)
        {
            ArgumentNullException.ThrowIfNull(bagPieceEntries);
            ArgumentNullException.ThrowIfNull(initialPieceTypes);

            List<BagPieceEntry> bagPieceEntriesCopy = new();

            foreach (BagPieceEntry bagPieceEntry in bagPieceEntries)
            {
                ArgumentNullException.ThrowIfNull(bagPieceEntry);

                bagPieceEntriesCopy.Add(bagPieceEntry);
            }

            _bagPieceEntries = bagPieceEntriesCopy;
            _initialPieceTypes = new List<PieceType>(initialPieceTypes);

            Refill();

            for (int i = _initialPieceTypes.Count - 1; i >= 0; --i)
            {
                _pieceTypes.Add(_initialPieceTypes[i]);
            }
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