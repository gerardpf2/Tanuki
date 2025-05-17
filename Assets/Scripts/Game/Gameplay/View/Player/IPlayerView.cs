using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using UnityEngine;

namespace Game.Gameplay.View.Player
{
    public interface IPlayerView
    {
        Coordinate Coordinate { get; }

        GameObject Instance { get; }

        void Initialize();

        void Instantiate(IPiece piece, GameObject prefab);

        void Destroy();

        void Move(float deltaX);
    }
}