using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public interface IBoardViewController
    {
        void Initialize(int rows, int columns);

        // TODO: Instantiate instead of Add
        void Add(IPiece piece, Coordinate sourceCoordinate, GameObject instance);
    }
}