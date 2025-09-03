using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public interface IBoardView : IReadonlyBoardView
    {
        void Initialize(IReadonlyBoard board);

        void Uninitialize();

        void InstantiatePiece(IPiece piece, Coordinate sourceCoordinate, GameObject prefab);
    }
}