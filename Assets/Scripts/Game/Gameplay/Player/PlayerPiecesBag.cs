using Game.Gameplay.Board;

namespace Game.Gameplay.Player
{
    public class PlayerPiecesBag : IPlayerPiecesBag
    {
        public void Initialize()
        {
            // TODO: Check allow multiple Initialize. Add Clear ¿?
        }

        public PieceType GetNext()
        {
            return PieceType.PlayerBlock;
        }
    }
}