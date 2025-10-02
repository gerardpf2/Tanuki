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
        IPiece GetPiece(uint id);

        [NotNull]
        GameObject GetPieceInstance(uint id);

        void InstantiatePiece(IPiece piece, Coordinate sourceCoordinate, GameObject prefab);

        void DestroyPiece(uint id);

        void MovePiece(uint id, int rowOffset, int columnOffset);
    }
}