using System.Collections.Generic;
using Game.Common;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Player
{
    public class PlayerPiecesBag : IPlayerPiecesBag
    {
        // TODO: Remove
        [NotNull] private static readonly IReadOnlyList<PieceType> PieceTypes =
            new List<PieceType>
            {
                PieceType.PlayerI,
                PieceType.PlayerO,
                PieceType.PlayerJ,
                PieceType.PlayerL,
                PieceType.Test
            };

        [NotNull] private readonly IPieceGetter _pieceGetter;

        private InitializedLabel _initializedLabel;

        // TODO: Remove
        private int _pieceTypeIndex = -1;

        public IPiece Current { get; private set; }

        public PlayerPiecesBag([NotNull] IPieceGetter pieceGetter)
        {
            ArgumentNullException.ThrowIfNull(pieceGetter);

            _pieceGetter = pieceGetter;
        }

        public void Initialize()
        {
            _initializedLabel.SetInitialized();
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            _pieceTypeIndex = -1;

            Current = null;
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

            _pieceTypeIndex = (_pieceTypeIndex + 1) % PieceTypes.Count;

            Current = _pieceGetter.Get(PieceTypes[_pieceTypeIndex]);
        }
    }
}