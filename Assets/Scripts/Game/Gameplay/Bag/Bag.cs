using System;
using System.Collections.Generic;
using Game.Common.Pieces;
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
        [NotNull] private readonly Random _random = new(); // TODO: IRandom dependency

        [NotNull, ItemNotNull] private readonly ICollection<BagPieceEntry> _bagPieceEntries = new List<BagPieceEntry>(); // ItemNotNull as long as all Add check for null
        [NotNull] private readonly List<PieceType> _initialPieceTypes = new();
        [NotNull, ItemNotNull] private readonly LinkedList<IPiece> _pieces = new(); // ItemNotNull as long as added items come from piece getter

        public IEnumerable<BagPieceEntry> BagPieceEntries => _bagPieceEntries;

        public IEnumerable<PieceType> InitialPieceTypes => _initialPieceTypes;

        public IPiece Current
        {
            get
            {
                LinkedListNode<IPiece> first = _pieces.First;

                InvalidOperationException.ThrowIfNull(first);

                return first.Value;
            }
            private set
            {
                ArgumentNullException.ThrowIfNull(value);

                LinkedListNode<IPiece> first = _pieces.First;

                InvalidOperationException.ThrowIfNull(first);

                first.Value = value;
            }
        }

        public IPiece Next
        {
            get
            {
                LinkedListNode<IPiece> second = _pieces.First?.Next;

                InvalidOperationException.ThrowIfNull(second);

                return second.Value;
            }
            private set
            {
                ArgumentNullException.ThrowIfNull(value);

                LinkedListNode<IPiece> second = _pieces.First?.Next;

                InvalidOperationException.ThrowIfNull(second);

                second.Value = value;
            }
        }

        public Bag([NotNull] IPieceGetter pieceGetter)
        {
            ArgumentNullException.ThrowIfNull(pieceGetter);

            _pieceGetter = pieceGetter;
        }

        public void Build(
            [NotNull, ItemNotNull] IEnumerable<BagPieceEntry> bagPieceEntries,
            [NotNull] IEnumerable<PieceType> initialPieceTypes)
        {
            ArgumentNullException.ThrowIfNull(bagPieceEntries);
            ArgumentNullException.ThrowIfNull(initialPieceTypes);

            Clear();

            foreach (BagPieceEntry bagPieceEntry in bagPieceEntries)
            {
                ArgumentNullException.ThrowIfNull(bagPieceEntry);

                _bagPieceEntries.Add(bagPieceEntry);
            }

            if (_bagPieceEntries.Count <= 0)
            {
                InvalidOperationException.Throw("Bag piece entries cannot be empty");
            }

            _initialPieceTypes.AddRange(initialPieceTypes);

            foreach (PieceType initialPieceType in _initialPieceTypes)
            {
                IPiece piece = GetPiece(initialPieceType);

                _pieces.AddLast(piece);
            }

            RefillIfNeeded();
        }

        public void ConsumeCurrent()
        {
            _pieces.RemoveFirst();

            RefillIfNeeded();
        }

        public void SwapCurrentNext()
        {
            (Current, Next) = (Next, Current);
        }

        public void Clear()
        {
            _bagPieceEntries.Clear();
            _initialPieceTypes.Clear();
            _pieces.Clear();
        }

        private void RefillIfNeeded()
        {
            const int minCount = 2; // Current, Next

            while (_pieces.Count < minCount)
            {
                RefillStep();
            }

            return;

            void RefillStep()
            {
                IList<IPiece> newPieces = new List<IPiece>();

                foreach (BagPieceEntry bagPieceEntry in _bagPieceEntries)
                {
                    for (int i = 0; i < bagPieceEntry.Amount; ++i)
                    {
                        IPiece piece = GetPiece(bagPieceEntry.PieceType);

                        newPieces.Add(piece);
                    }
                }

                newPieces.Shuffle(_random);

                foreach (IPiece piece in newPieces)
                {
                    _pieces.AddLast(piece);
                }
            }
        }

        [NotNull]
        private IPiece GetPiece(PieceType pieceType)
        {
            return _pieceGetter.Get(pieceType);
        }
    }
}