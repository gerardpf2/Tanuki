using System;
using Game.Gameplay.Board;
using Game.Gameplay.Pieces.Pieces;

namespace Game.Gameplay.View.Player
{
    public interface IPlayerPieceView : IBasePlayerPieceView
    {
        event Action OnMoved;
        event Action OnRotated;

        void Instantiate(IPiece piece, Coordinate sourceCoordinate);

        bool CanMove(int columnOffset);

        void Move(int columnOffset);

        bool CanRotate();

        void Rotate();
    }
}