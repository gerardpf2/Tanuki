using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public interface IBoardView
    {
        void Initialize();

        void Uninitialize();

        [NotNull]
        IPiece GetPiece(int id);

        [NotNull]
        GameObject GetPieceInstance(int id);

        void InstantiatePiece(IPiece piece, Coordinate sourceCoordinate, GameObject prefab);

        void DestroyPiece(int id);

        void MovePiece(int id, int rowOffset, int columnOffset);
    }
}