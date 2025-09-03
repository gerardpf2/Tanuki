using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public interface IBoardViewController
    {
        void Initialize(int rows, int columns, Transform piecesParent);

        GameObject Instantiate(IPiece piece, Coordinate sourceCoordinate, GameObject prefab);
    }
}