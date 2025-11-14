using System;
using Game.Gameplay.Board;
using Game.Gameplay.Pieces.Pieces;
using UnityEngine;

namespace Game.Gameplay.View.Player
{
    public interface IPlayerPieceView
    {
        event Action OnInstantiated;
        event Action OnDestroyed;
        event Action OnMoved;
        event Action OnRotated;

        Coordinate Coordinate { get; }

        GameObject Instance { get; }

        void Initialize();

        void Uninitialize();

        void Instantiate(IPiece piece, Coordinate sourceCoordinate);

        void Destroy();

        bool CanMove(int offsetX);

        void Move(int offsetX);

        bool CanRotate();

        void Rotate();
    }
}