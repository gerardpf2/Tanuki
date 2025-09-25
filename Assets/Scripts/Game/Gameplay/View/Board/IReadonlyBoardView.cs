using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public interface IReadonlyBoardView
    {
        IBoard Board { get; }

        [NotNull]
        GameObject GetPieceInstance(IPiece piece);
    }
}