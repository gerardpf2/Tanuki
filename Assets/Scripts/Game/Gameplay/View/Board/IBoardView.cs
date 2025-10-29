using Game.Gameplay.Board;
using Game.Gameplay.Pieces.Pieces;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public interface IBoardView
    {
        void Initialize();

        void Uninitialize();

        [NotNull]
        IPiece GetPiece(int pieceId);

        [NotNull]
        GameObject GetPieceInstance(int pieceId);

        void InstantiatePiece(IPiece piece, Coordinate sourceCoordinate, GameObject prefab);

        void DestroyPiece(int pieceId);

        void MovePiece(int pieceId, int rowOffset, int columnOffset);
    }
}