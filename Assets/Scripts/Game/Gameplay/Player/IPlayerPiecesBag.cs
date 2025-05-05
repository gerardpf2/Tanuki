using Game.Gameplay.Board;

namespace Game.Gameplay.Player
{
    public interface IPlayerPiecesBag
    {
        void Initialize();

        PieceType GetNext();
    }
}