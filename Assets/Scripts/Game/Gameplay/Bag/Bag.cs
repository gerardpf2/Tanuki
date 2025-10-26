using System;
using System.Collections.Generic;
using Game.Gameplay.Pieces;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.List.Utils;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.Bag
{
    public class Bag : IBag
    {
        [NotNull] private readonly IPieceGetter _pieceGetter;
        [NotNull, ItemNotNull] private readonly IReadOnlyCollection<BagPieceEntry> _bagPieceEntries;
        [NotNull] private readonly IReadOnlyList<PieceType> _initialPieceTypes;
        [NotNull] private readonly Random _random = new();

        /*
         *
         * ItemNotNull as long as added items come from piece getter
         * Reverse order because add / remove last should be more efficient than first
         *
         */
        [NotNull, ItemNotNull] private readonly IList<IPiece> _pieces = new List<IPiece>();

        public IEnumerable<BagPieceEntry> BagPieceEntries => _bagPieceEntries;

        public IEnumerable<PieceType> InitialPieceTypes => _initialPieceTypes;

        public IPiece Current
        {
            get
            {
                if (_pieces.Count == 0)
                {
                    InvalidOperationException.Throw("Cannot be empty");
                }

                return _pieces[^1];
            }
        }

        public Bag(
            [NotNull] IPieceGetter pieceGetter,
            [NotNull, ItemNotNull] IEnumerable<BagPieceEntry> bagPieceEntries,
            [NotNull] IEnumerable<PieceType> initialPieceTypes)
        {
            ArgumentNullException.ThrowIfNull(pieceGetter);
            ArgumentNullException.ThrowIfNull(bagPieceEntries);
            ArgumentNullException.ThrowIfNull(initialPieceTypes);

            List<BagPieceEntry> bagPieceEntriesCopy = new();

            foreach (BagPieceEntry bagPieceEntry in bagPieceEntries)
            {
                ArgumentNullException.ThrowIfNull(bagPieceEntry);

                bagPieceEntriesCopy.Add(bagPieceEntry);
            }

            _pieceGetter = pieceGetter;
            _bagPieceEntries = bagPieceEntriesCopy;
            _initialPieceTypes = new List<PieceType>(initialPieceTypes);

            Refill();

            for (int i = _initialPieceTypes.Count - 1; i >= 0; --i)
            {
                IPiece piece = GetPiece(_initialPieceTypes[i]);

                _pieces.Add(piece);
            }
        }

        public void ConsumeCurrent()
        {
            if (_pieces.Count == 0)
            {
                InvalidOperationException.Throw("Cannot be empty");
            }

            _pieces.RemoveAt(_pieces.Count - 1);

            if (_pieces.Count == 0)
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
                    IPiece piece = GetPiece(bagPieceEntry.PieceType);

                    _pieces.Add(piece);
                }
            }

            Shuffle();
        }

        private void Shuffle()
        {
            _pieces.Shuffle(_random);
        }

        [NotNull]
        private IPiece GetPiece(PieceType pieceType)
        {
            return _pieceGetter.Get(pieceType);
        }
    }
}