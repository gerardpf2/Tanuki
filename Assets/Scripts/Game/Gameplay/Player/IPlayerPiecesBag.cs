using Game.Gameplay.Board;

namespace Game.Gameplay.Player
{
    public interface IPlayerPiecesBag
    {
        PieceType? Current { get; }

        void Initialize();

        void ConsumeCurrent();

        void PrepareNext();
    }
}