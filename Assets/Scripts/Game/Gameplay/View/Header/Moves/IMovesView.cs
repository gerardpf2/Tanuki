using System;

namespace Game.Gameplay.View.Header.Moves
{
    public interface IMovesView
    {
        event Action OnUpdated;

        int Amount { get; set; }

        void Initialize();

        void Uninitialize();
    }
}