using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using UnityEngine;

namespace Game.Gameplay.View.Player
{
    public interface IPlayerView
    {
        Coordinate PieceCoordinate { get; }

        GameObject PieceInstance { get; }

        void Initialize();

        void Uninitialize();

        void InstantiatePiece(IPiece piece, GameObject prefab);

        void DestroyPiece();

        void MovePiece(float deltaX);
    }
}