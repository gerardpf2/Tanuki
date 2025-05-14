using Game.Gameplay.Board;
using Infrastructure.System.Exceptions;

namespace Game.Gameplay.Player
{
    public class PlayerPiecesBag : IPlayerPiecesBag
    {
        public PieceType? Current { get; private set; }

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

            Current = PieceType.PlayerBlock;
        }
    }
}