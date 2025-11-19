using System;
using Game.Gameplay.Board;
using UnityEngine;

namespace Game.Gameplay.View.Player
{
    public interface IBasePlayerPieceView
    {
        event Action OnInstantiated;
        event Action OnDestroyed;

        Coordinate Coordinate { get; }

        GameObject Instance { get; }

        void Initialize();

        void Uninitialize();
        
        void Destroy();
    }
}