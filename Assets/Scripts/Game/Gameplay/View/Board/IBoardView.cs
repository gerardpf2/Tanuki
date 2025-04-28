using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public interface IBoardView
    {
        void Initialize(int rows, int columns);

        void Add(IPiece piece, Coordinate sourceCoordinate, GameObject instance);
    }
}