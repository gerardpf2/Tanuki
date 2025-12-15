using Game.Gameplay.Pieces.Pieces;

namespace Game.Gameplay.View.Player
{
    public interface IPlayerPieceGhostView : IBasePlayerPieceView
    {
        void Instantiate(IPiece piece);
    }
}