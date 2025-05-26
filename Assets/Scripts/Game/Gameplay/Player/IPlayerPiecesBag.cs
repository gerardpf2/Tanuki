using Game.Gameplay.Board.Pieces;

namespace Game.Gameplay.Player
{
    public interface IPlayerPiecesBag
    {
        IPiece Current { get; }

        void Initialize();

        void Uninitialize();

        void ConsumeCurrent();

        void PrepareNext();
    }
}