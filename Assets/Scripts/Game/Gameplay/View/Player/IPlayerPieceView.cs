using Game.Gameplay.Board;
using Game.Gameplay.Pieces.Pieces;
using UnityEngine;

namespace Game.Gameplay.View.Player
{
    public interface IPlayerPieceView
    {
        Coordinate Coordinate { get; }

        GameObject Instance { get; }

        void Initialize();

        void Uninitialize();

        void Instantiate(IPiece piece);

        void Destroy();

        void Move(float deltaX);

        void Rotate();
    }
}