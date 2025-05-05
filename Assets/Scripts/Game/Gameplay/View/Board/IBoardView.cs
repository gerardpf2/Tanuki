using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public interface IBoardView
    {
        void Initialize(IReadonlyBoard board);

        [NotNull]
        GameObject Instantiate(IPiece piece, Coordinate sourceCoordinate, GameObject prefab);
    }
}