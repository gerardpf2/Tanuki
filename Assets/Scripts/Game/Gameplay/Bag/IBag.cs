using Game.Gameplay.Board;

namespace Game.Gameplay.Bag
{
    public interface IBag
    {
        PieceType GetCurrent();

        void ConsumeCurrent();
    }
}