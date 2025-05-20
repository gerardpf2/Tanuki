using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Player
{
    public class PlayerPiecesBag : IPlayerPiecesBag
    {
        [NotNull] private readonly IPieceGetter _pieceGetter;

        public IPiece Current { get; private set; }

        public PlayerPiecesBag([NotNull] IPieceGetter pieceGetter)
        {
            ArgumentNullException.ThrowIfNull(pieceGetter);

            _pieceGetter = pieceGetter;
        }

        public void Initialize()
        {
            // TODO: Check allow multiple Initialize. Add Clear ¿?
        }

        public void ConsumeCurrent()
        {
            InvalidOperationException.ThrowIfNull(Current);

            Current = null;
        }

        public void PrepareNext()
        {
            InvalidOperationException.ThrowIfNotNull(Current);

            Current = _pieceGetter.Get(PieceType.PlayerBlock11);
        }
    }
}