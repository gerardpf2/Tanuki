using Game.Gameplay.Board;
using Game.Gameplay.Pieces.Pieces;
using UnityEngine;

namespace Game.Gameplay.View.Player
{
    public interface IPlayerPieceGhostView
    {
        Coordinate Coordinate { get; }

        GameObject Instance { get; }

        void Initialize();

        void Uninitialize();

        void Instantiate(IPiece piece);

        void Destroy();
    }
}