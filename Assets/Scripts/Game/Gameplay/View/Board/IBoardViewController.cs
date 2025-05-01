using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public interface IBoardViewController
    {
        void Initialize(int rows, int columns, Transform piecesParent);

        [NotNull]
        GameObject Instantiate(IPiece piece, Coordinate sourceCoordinate, GameObject prefab);
    }
}