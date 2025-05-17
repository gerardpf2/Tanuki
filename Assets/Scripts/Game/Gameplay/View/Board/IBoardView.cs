using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public interface IBoardView : IReadonlyBoardView
    {
        void Initialize(IReadonlyBoard board);

        [NotNull]
        GameObject InstantiatePiece(IPiece piece, Coordinate sourceCoordinate, GameObject prefab);
    }
}