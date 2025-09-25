using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public interface IBoardView
    {
        IBoard Board { get; }

        void Initialize();

        void Uninitialize();

        [NotNull]
        GameObject GetPieceInstance(IPiece piece);

        void InstantiatePiece(IPiece piece, Coordinate sourceCoordinate, GameObject prefab);

        void DestroyPiece(IPiece piece);

        void MovePiece(IPiece piece, int rowOffset, int columnOffset);
    }
}