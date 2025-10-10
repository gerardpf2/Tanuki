using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using UnityEngine;

namespace Game.Gameplay.View.Player
{
    public interface IPiecePlayerView
    {
        Coordinate Coordinate { get; }

        GameObject Instance { get; }

        void Initialize();

        void Uninitialize();

        void Instantiate(IPiece piece, GameObject prefab);

        void Destroy();

        void Move(float deltaX);
    }
}