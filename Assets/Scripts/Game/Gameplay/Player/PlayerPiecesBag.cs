using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Player
{
    public class PlayerPiecesBag : IPlayerPiecesBag
    {
        [NotNull] private readonly IPieceGetter _pieceGetter;

        // TODO: Remove
        [NotNull] private readonly IReadOnlyList<PieceType> _pieceTypes =
            new List<PieceType>
            {
                PieceType.PlayerBlock11,
                PieceType.PlayerBlock12,
                PieceType.PlayerBlock21
            };

        public IPiece Current { get; private set; }

        // TODO: Remove
        private int _pieceTypeIndex = -1;

        public PlayerPiecesBag([NotNull] IPieceGetter pieceGetter)
        {
            ArgumentNullException.ThrowIfNull(pieceGetter);

            _pieceGetter = pieceGetter;
        }

        public void Initialize()
        {
            // TODO: Remove if not needed

            Uninitialize();
        }

        public void Uninitialize()
        {
            Current = null;

            _pieceTypeIndex = -1;
        }

        public void ConsumeCurrent()
        {
            InvalidOperationException.ThrowIfNull(Current);

            Current = null;
        }

        public void PrepareNext()
        {
            // TODO: Random bag, fill bag, hardcoded initial pieces, etc

            InvalidOperationException.ThrowIfNotNull(Current);

            _pieceTypeIndex = (_pieceTypeIndex + 1) % _pieceTypes.Count;

            Current = _pieceGetter.Get(_pieceTypes[_pieceTypeIndex], null); // No state
        }
    }
}