using System;

namespace Game.Gameplay.View.Player.Input.ActionHandlers
{
    public interface IPlayerInputActionHandler
    {
        event Action OnAvailableUpdated;

        bool Available { get; }

        void Initialize();

        void Uninitialize();

        void Resolve();
    }
}