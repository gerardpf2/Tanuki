using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public interface IBoardView : IReadonlyBoardView
    {
        void Initialize();

        void Uninitialize();

        void InstantiatePiece(IPiece piece, Coordinate sourceCoordinate, GameObject prefab);

        void DestroyPiece(IPiece piece);

        void MovePiece(IPiece piece, int rowOffset, int columnOffset);
    }
}