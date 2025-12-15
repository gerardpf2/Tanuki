using System;
using Game.Gameplay.Board;
using Game.Gameplay.Pieces.Pieces;
using UnityEngine;

namespace Game.Gameplay.View.Player
{
    public interface IBasePlayerPieceView
    {
        event Action OnInstantiated;
        event Action OnDestroyed;

        IPiece Piece { get; }

        Coordinate Coordinate { get; }

        GameObject Instance { get; }

        void Initialize();

        void Uninitialize();

        void Destroy();
    }
}